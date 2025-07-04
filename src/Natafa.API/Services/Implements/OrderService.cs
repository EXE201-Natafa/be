using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Claims;
using Natafa.Api.Models;
using CloudinaryDotNet.Actions;

namespace Natafa.Api.Services.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IShippingService _shippingService;

        public OrderService(IUnitOfWork uow, IMapper mapper, IShippingService shippingService)
        {
            _uow = uow;
            _mapper = mapper;
            _shippingService = shippingService;
        }

        public async Task<MethodResult<string>> CreateOrderAsync(int userId, int? voucherId, OrderCreateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var order = _mapper.Map<Order>(request);
                order.UserId = userId;

                await HandleOrderDetailAsync(order);
                await HandleShippingPriceAsync(request.InRegion, request.OrderDetailRequests, order);
                await HandleVoucherAsync(userId, voucherId, order);

                await _uow.GetRepository<Order>().InsertAsync(order);
                await CreateOrderTrackingAsync(order);
                await _uow.CommitAsync();

                order.OrderCode = $"ORD{DateTime.Now.ToString("yyyyMMdd")}{order.OrderId}";
                _uow.GetRepository<Order>().UpdateAsync(order);
                await _uow.CommitAsync();

                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Order created successfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleOrderDetailAsync(Order order)
        {
            foreach (var item in order.OrderDetails)
            {
                var productDetail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(
                    predicate: p => p.ProductDetailId == item.ProductDetailId
                );
                if (productDetail == null)
                {
                    throw new Exception($"Product detail {item.ProductDetailId} not found");
                }
                if (productDetail.Quantity < item.Quantity)
                {
                    throw new Exception($"Product detail {item.ProductDetailId} do not enough quantity");
                }
                productDetail.Quantity -= item.Quantity;

                _uow.GetRepository<ProductDetail>().UpdateAsync(productDetail);

                item.Price = productDetail.Price * (1 - productDetail.Discount / 100);
                order.TotalAmount += item.Price * item.Quantity;
            }
        }

        private async Task HandleShippingPriceAsync(bool inRegion, List<OrderDetailCreateRequest> request, Order order)
        {
            order.ShippingPrice = await _shippingService.GetShippingCostAsync(inRegion, request);
            order.TotalAmount += order.ShippingPrice;
        }
        private async Task HandleVoucherAsync(int userId, int? voucherId, Order order)
        {
            if (voucherId.HasValue)
            {
                var voucher = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                    predicate: p => p.VoucherId == voucherId
                );
                if (voucher == null)
                {
                    throw new Exception("Voucher not found");
                }                               
                if (!voucher.Status)
                {
                    throw new Exception("Voucher is inactive");
                }
                if (voucher.StartDate > DateTime.Now || voucher.EndDate < DateTime.Now)
                {
                    throw new Exception("Invalid date of voucher");
                }
                if (voucher.MinimumPurchase > order.TotalAmount)
                {
                    throw new Exception("Order do not enough amount to apply voucher");
                }

                var userVoucher = await _uow.GetRepository<UserVoucher>().SingleOrDefaultAsync(
                    predicate: p => p.VoucherId == voucherId && p.UserId == userId
                );
                if (userVoucher == null)
                {
                    throw new Exception("User do not have this voucher");
                }
                if (userVoucher.Status)
                {
                    throw new Exception("Voucher was used before");
                }

                order.VoucherId = voucherId;
                order.TotalAmount -= voucher.DiscountAmount;
                await UpdateUserVoucher(userVoucher);
            }
        }

        private async Task UpdateUserVoucher(UserVoucher userVoucher)
        {
            userVoucher.RedeemedDate = DateTime.Now;
            userVoucher.Status = true;
            _uow.GetRepository<UserVoucher>().UpdateAsync(userVoucher);
        }

        private async Task CreateOrderTrackingAsync(Order order)
        {
            //var orderTracking = new OrderTracking
            //{
            //    OrderId = order.OrderId,
            //    Status = OrderConstant.ORDER_STATUS_PENDING,
            //    UpdatedDate = DateTime.Now
            //};
            //await _uow.GetRepository<OrderTracking>().InsertAsync(orderTracking);
            order.OrderTrackings.Add(new OrderTracking
            {
                Status = OrderConstant.ORDER_STATUS_PENDING,
                UpdatedDate = DateTime.Now
            });
        }

        public async Task<MethodResult<IPaginate<OrderResponse>>> GetOrdersByUserAsync(int userId, PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Order, bool>> predicate = p =>
                p.UserId == userId &&
                (string.IsNullOrEmpty(search) || p.User.FullName.ToLower().Contains(search) ||
                                                       p.User.Email.ToLower().Contains(search) ||
                                                       p.Address.ToLower().Contains(search) ||
                                                       p.PhoneNumber.ToLower().Contains(search)) &&
                (string.IsNullOrEmpty(filter) || filter.Contains(p.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().Status.ToLower())) &&
                (minAmount == null || p.TotalAmount >= minAmount) &&
                (maxAmount == null || p.TotalAmount <= maxAmount);

            var result = await _uow.GetRepository<Order>().GetPagingListAsync<OrderResponse>(
                    selector: s => _mapper.Map<OrderResponse>(s),
                    predicate: predicate,
                    include: i => i.Include(x => x.OrderDetails)
                                   .Include(x => x.OrderTrackings)
                                   .Include(x => x.User)
                                   .Include(x => x.Transactions)
                                   .Include(x => x.Voucher),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );

            return new MethodResult<IPaginate<OrderResponse>>.Success(result);
        }

        public async Task<MethodResult<IPaginate<OrderResponse>>> GetAllOrdersAsync(PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Order, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.User.FullName.Contains(search) ||
                                                 p.User.Email.Contains(search) ||
                                                 p.Address.Contains(search) ||
                                                 p.PhoneNumber.Contains(search)) &&
                (string.IsNullOrEmpty(filter) ||
                filter.Contains(p.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().Status.ToLower())) &&
                (minAmount == null || p.TotalAmount >= minAmount) &&
                (maxAmount == null || p.TotalAmount <= maxAmount);

            var result = await _uow.GetRepository<Order>().GetPagingListAsync<OrderResponse>(
                    selector: s => _mapper.Map<OrderResponse>(s),
                    predicate: predicate,
                    include: i => i.Include(x => x.OrderDetails)
                                   .Include(x => x.OrderTrackings)
                                   .Include(x => x.User)
                                   .Include(x => x.Transactions)
                                   .Include(x => x.Voucher),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );
            return new MethodResult<IPaginate<OrderResponse>>.Success(result);
        }

        private Func<IQueryable<Order>, IOrderedQueryable<Order>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return q => q.OrderByDescending(p => p.OrderId);

            return sortBy.ToLower() switch
            {
                "amount" => q => q.OrderBy(p => p.TotalAmount),
                "amount_desc" => q => q.OrderByDescending(p => p.TotalAmount),
                "date" => q => q.OrderBy(p => p.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().UpdatedDate),
                "date_desc" => q => q.OrderByDescending(p => p.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().UpdatedDate),
                _ => q => q.OrderByDescending(p => p.OrderId) // Default sort
            };
        }

        public async Task<MethodResult<string>> CompleteOrderAsync(int userId, int orderId, string role)
        {
            try
            {
                var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId
                );
                if (order == null)
                {
                    return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
                }
                if (order.UserId != userId && role == UserConstant.USER_ROLE_CUSTOMER)
                {
                    return new MethodResult<string>.Failure("User do not have this order", StatusCodes.Status404NotFound);
                }

                var orderStatusCurrent = await _uow.GetRepository<OrderTracking>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    orderBy: o => o.OrderByDescending(x => x.UpdatedDate)
                );
                if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_SHIPPING)
                {
                    return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}", StatusCodes.Status400BadRequest);
                }

                var orderTracking = new OrderTracking
                {
                    OrderId = orderId,
                    UpdatedDate = DateTime.Now,
                    Status = OrderConstant.ORDER_STATUS_COMPLETED,
                };

                await _uow.GetRepository<OrderTracking>().InsertAsync(orderTracking);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Complete order successfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> CancelOrderAsync(string role, int userId, int orderId)
        {
            try
            {
                var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    include: i => i.Include(x => x.OrderDetails).ThenInclude(x => x.ProductDetail)
                );
                if (order == null)
                {
                    return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
                }

                var orderStatusCurrent = await _uow.GetRepository<OrderTracking>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    orderBy: o => o.OrderByDescending(x => x.UpdatedDate)
                );

                if (role == UserConstant.USER_ROLE_CUSTOMER)
                {
                    if (order.UserId != userId)
                    {
                        return new MethodResult<string>.Failure($"You do not own this order", StatusCodes.Status403Forbidden);
                    }

                    if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PENDING)
                    {
                        return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}. You can not cancel order", StatusCodes.Status400BadRequest);
                    }
                }
                else
                {
                    if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PENDING && orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PAID)
                    {
                        return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}. You can not cancel order", StatusCodes.Status400BadRequest);
                    }

                    if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PAID)
                    {
                        //await RefundOrderAsync(order);
                    }
                }

                await HandleCancelOrderAsync(order);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Cancel order successfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        //private async Task RefundOrderAsync(Order order)
        //{
        //    try
        //    {
        //        var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
        //            predicate: p => p.UserId == order.UserId
        //        );
        //        user.WalletBalance += order.TotalAmount;

        //        var transaction = new Transaction
        //        {
        //            UserId = user.UserId,
        //            Amount = order.TotalAmount,
        //            CreateDate = DateTime.Now,
        //            Type = TransactionConstant.TRANSACTION_TYPE_REFUND,
        //            RelatedId = order.OrderId,
        //            Description = $"Refund for order {order.OrderId} with toal amount {order.TotalAmount}"
        //        };

        //        _uow.GetRepository<User>().UpdateAsync(user);
        //        _uow.GetRepository<Transaction>().UpdateAsync(transaction);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private async Task HandleCancelOrderAsync(Order order)
        {
            try
            {
                foreach (var detail in order.OrderDetails)
                {
                    var productDetail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(
                        predicate: p => p.ProductDetailId == detail.ProductDetailId
                    );
                    productDetail.Quantity += detail.Quantity;
                }

                var orderTracking = new OrderTracking
                {
                    OrderId = order.OrderId,
                    UpdatedDate = DateTime.Now,
                    Status = OrderConstant.ORDER_STATUS_CANCELED,
                };

                await _uow.GetRepository<OrderTracking>().InsertAsync(orderTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MethodResult<string>> ConfirmOrderAsync(int orderId)
        {
            try
            {
                var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId
                );
                if (order == null)
                {
                    return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
                }

                var orderStatusCurrent = await _uow.GetRepository<OrderTracking>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    orderBy: o => o.OrderByDescending(x => x.UpdatedDate)
                );

                if (order.PaymentMethodId == 1)
                {
                    if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PENDING)
                    {
                        return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}", StatusCodes.Status400BadRequest);
                    }
                }
                else
                {
                    if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_PAID)
                    {
                        return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}. Order owner has not make payment", StatusCodes.Status400BadRequest);
                    }
                }

                var orderTracking = new OrderTracking
                {
                    OrderId = orderId,
                    UpdatedDate = DateTime.Now,
                    Status = OrderConstant.ORDER_STATUS_SHIPPING,
                };

                await _uow.GetRepository<OrderTracking>().InsertAsync(orderTracking);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Confirm order successfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> ReturnOrderAsync(string role, int userId, int orderId)
        {
            try
            {
                var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    include: i => i.Include(x => x.OrderDetails).ThenInclude(x => x.ProductDetail)
                );
                if (order == null)
                {
                    return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
                }

                var orderStatusCurrent = await _uow.GetRepository<OrderTracking>().SingleOrDefaultAsync(
                    predicate: p => p.OrderId == orderId,
                    orderBy: o => o.OrderByDescending(x => x.UpdatedDate)
                );

                if (role == UserConstant.USER_ROLE_CUSTOMER)
                {
                    if (order.UserId != userId)
                    {
                        return new MethodResult<string>.Failure($"You do not own this order", StatusCodes.Status403Forbidden);
                    }
                }
                
                if (orderStatusCurrent.Status != OrderConstant.ORDER_STATUS_SHIPPING)
                {
                    return new MethodResult<string>.Failure($"Order status is {orderStatusCurrent.Status}. You can not return order", StatusCodes.Status400BadRequest);
                }
                               
                await HandleReturnOrderAsync(order);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Return order successfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleReturnOrderAsync(Order order)
        {
            try
            {
                foreach (var detail in order.OrderDetails)
                {
                    var productDetail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(
                        predicate: p => p.ProductDetailId == detail.ProductDetailId
                    );
                    productDetail.Quantity += detail.Quantity;
                }

                var orderTracking = new OrderTracking
                {
                    OrderId = order.OrderId,
                    UpdatedDate = DateTime.Now,
                    Status = OrderConstant.ORDER_STATUS_RETURNED,
                };

                await _uow.GetRepository<OrderTracking>().InsertAsync(orderTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MethodResult<string>> GetShippingCostAsync(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            var result = await _shippingService.GetShippingCostAsync(inRegion, request);
            return new MethodResult<string>.Success(result.ToString());
        }
    }
}
