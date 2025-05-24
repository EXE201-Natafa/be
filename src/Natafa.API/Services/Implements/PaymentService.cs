//using Natafa.Api.Constants;
//using Natafa.Api.Helper;
//using Natafa.Api.Models.Configurations;
//using Natafa.Api.Models.VnPayModel;
//using Natafa.Api.Services.Interfaces;
//using Natafa.Domain.Entities;
//using Natafa.Repository.Interfaces;
//using MailKit.Search;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Natafa.Api.Services.Implements
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly VnPayConfig _config;
//        private readonly IUnitOfWork _uow;

//        public PaymentService(IOptions<VnPayConfig> options, IUnitOfWork uow)
//        {          
//            _config = options.Value;
//            _uow = uow;
//        }

//        public async Task<MethodResult<string>> CreatePaymentAsync(string email, int orderId, string type, HttpContext httpContext)
//        {            
//            var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
//                predicate: p => p.OrderId == orderId
//            );

//            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
//                predicate: p => p.Email == email
//            );

//            if (order.UserId != user.UserId)
//            {
//                return new MethodResult<string>.Failure("User have not this order", 400);
//            }

//            var orderStatus = await _uow.GetRepository<OrderStatus>().SingleOrDefaultAsync<string>(
//                selector: s => s.Status,
//                predicate: p => p.OrderId == orderId,
//                orderBy: o => o.OrderByDescending(x => x.UpdateTime)
//            );
//            if (orderStatus != OrderConstant.ORDER_STATUS_PENDING)
//            {
//                return new MethodResult<string>.Failure($"Order Status is {orderStatus}", 400);
//            }

//            if (type == "Wallet")
//            {
//                if (user.WalletBalance < order.TotalAmount)
//                {
//                    return new MethodResult<string>.Failure($"Wallet is not enough for order", 400);
//                }

//                user.WalletBalance -= order.TotalAmount;
//                _uow.GetRepository<User>().UpdateAsync(user);

//                await CreateOrderStatusAsync(order);
//                await CreateDepositTransactionAsync(order);

//                await _uow.CommitAsync();
//                return new MethodResult<string>.Success("Payment success. Transaction Created");
//            }
//            else if (type == "Order")
//            {
//                var vnpayModel = new VnPaymentRequestModel
//                {
//                    Amount = order.TotalAmount,
//                    RelateId = orderId,
//                    CreatedDate = DateTime.Now,
//                    Type = "Order"
//                };

//                return await CreatePaymentUrl(httpContext, vnpayModel);
//            }
            
//             return new MethodResult<string>.Failure("Invalid type param", StatusCodes.Status500InternalServerError);            
//        } 

//        public async Task<MethodResult<string>> AddToWalletAsync(string email, decimal amount, HttpContext httpContext)
//        {
//            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
//                predicate: p => p.Email == email
//            );

//            var vnpayModel = new VnPaymentRequestModel
//            {
//                Amount = amount,
//                RelateId = user.UserId,
//                CreatedDate = DateTime.Now,
//                Type = "Wallet"
//            };

//            return await CreatePaymentUrl(httpContext, vnpayModel);
//        }

//        private async Task<MethodResult<string>> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
//        {
//            var tick = model.Type + DateTime.Now.Ticks.ToString();

//            var vnpay = new VnPayLibrary();

//            vnpay.AddRequestData("vnp_Version", _config.Version);
//            vnpay.AddRequestData("vnp_Command", _config.Command);
//            vnpay.AddRequestData("vnp_TmnCode", _config.TmnCode);
//            vnpay.AddRequestData("vnp_Amount", ((float)model.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY là: 10000000                
//            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
//            vnpay.AddRequestData("vnp_CurrCode", _config.CurrCode);
//            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
//            vnpay.AddRequestData("vnp_Locale", _config.Locale);

//            if (model.Type == "Order")
//            {
//                vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán chuyển khoản cho Order có ID {model.RelateId} với số tiền {model.Amount}");
//            }
//            else if (model.Type == "Wallet")
//            {
//                vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán nạp tiền cho User có ID {model.RelateId} với số tiền {model.Amount}");
//            }
//            else
//            {
//                return new MethodResult<string>.Failure("Type vnpay request is invalid", StatusCodes.Status500InternalServerError);
//            }

//            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
//            vnpay.AddRequestData("vnp_ReturnUrl", _config.ReturnUrl);
//            vnpay.AddRequestData("vnp_TxnRef", tick); // Mã tham chiếu của giao dịch tại hệ thống của merchant.Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY.Không được trùng lặp trong ngày    
//            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

//            var paymentUrl = vnpay.CreateRequestUrl(_config.BaseUrl, _config.HashSecret);

//            return await Task.FromResult(new MethodResult<string>.Success(paymentUrl));
//        }

//        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
//        {
//            var vnpay = new VnPayLibrary();

//            foreach (var (key, value) in collections)
//            {
//                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
//                {
//                    vnpay.AddResponseData(key, value.ToString());
//                }
//            }

//            var vnp_OrderRefId = vnpay.GetResponseData("vnp_TxnRef");
//            var vnp_TransactionId = vnpay.GetResponseData("vnp_TransactionNo");       
//            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
//            var vnp_SecureHash = collections.FirstOrDefault(x => x.Key == "vnp_SecureHash").Value;
//            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo").ToString();
//            var vnp_Amount = vnpay.GetResponseData("vnp_Amount");

//            var type = Regex.Match(vnp_OrderRefId, @"^[^\d]+").Value;

//            string pattern = @"ID \s*(\d+)\s* với số tiền";
//            Regex regex = new Regex(pattern);
//            Match match = regex.Match(vnp_OrderInfo);
//            string vnp_RelateId = match.Groups[1].Value;

//            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config.HashSecret);

//            if (!checkSignature)
//            {
//                return new VnPaymentResponseModel
//                {
//                    Success = false
//                };
//            }

//            return new VnPaymentResponseModel
//            {
//                Success = true,
//                PaymentMethod = "VnPay",
//                OrderDescription = vnp_OrderInfo,
//                OrderRefId = vnp_OrderRefId.ToString(),
//                TransactionId = vnp_TransactionId.ToString(),
//                Token = vnp_SecureHash.ToString(),
//                VnPayResponseCode = vnp_ResponseCode.ToString(),
//                Amount = decimal.Parse(vnp_Amount) / 100,
//                Type = type,
//                RelateId = int.Parse(vnp_RelateId)
//            };
//        }

//        public async Task<string> ProcessResponseAsync(VnPaymentResponseModel response)
//        {
//            try
//            {
//                string type;
//                string message;

//                if (response.VnPayResponseCode != "00")
//                {
//                    type = "fail";
//                    message = $"Payment fail with VnpayRespondCode: {response.VnPayResponseCode}";
//                }
//                else
//                {
//                    type = "success";

//                    if (response.Type == "Order")
//                    {
//                        var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
//                            predicate: p => p.OrderId == response.RelateId
//                        );
//                        await CreateOrderStatusAsync(order);
//                        var transactionId = await CreateDeductionTransactionAsync(order, response);

//                        message = $"{transactionId}";
//                    }
//                    else
//                    {
//                        await UpdateWalletUserAsync(response);
//                        var transactionId = await CreateWalletTransactionAsync(response);

//                        message = $"{transactionId}";
//                    }
//                }

//                return $"payment-result?type={type}&message={message}";
//            }
//            catch (Exception e)
//            {
//                return $"payment-result?type=fail&message=Payment fail: {e}";
//            }            
//        }

//        private async Task CreateOrderStatusAsync(Order order)
//        {
//            var orderStatus = new OrderStatus
//            {
//                OrderId = order.OrderId,
//                Status = OrderConstant.ORDER_STATUS_PAID,
//                UpdateTime = DateTime.Now
//            };

//            await _uow.GetRepository<OrderStatus>().InsertAsync(orderStatus);
//        }

//        private async Task<int> CreateDeductionTransactionAsync(Order order, VnPaymentResponseModel response)
//        {
//            var transaction = new Transaction
//            {
//                Amount = order.TotalAmount,
//                CreateDate = DateTime.Now,
//                RelatedId = order.OrderId,
//                Type = TransactionConstant.TRANSACTION_TYPE_DEDUCTION,
//                UserId = order.UserId,
//                Description = response.OrderDescription
//            };

//            await _uow.GetRepository<Transaction>().InsertAsync(transaction);
//            await _uow.CommitAsync();
//            return transaction.TransactionId;
//        }

//        private async Task<int> CreateWalletTransactionAsync(VnPaymentResponseModel response)
//        {
//            var transaction = new Transaction
//            {
//                Amount = response.Amount,
//                CreateDate = DateTime.Now,
//                Type = TransactionConstant.TRANSACTION_TYPE_WALLET,
//                UserId = response.RelateId,
//                Description = response.OrderDescription
//            };

//            await _uow.GetRepository<Transaction>().InsertAsync(transaction);
//            await _uow.CommitAsync();
//            return transaction.TransactionId;
//        }

//        private async Task CreateDepositTransactionAsync(Order order)
//        {
//            var transaction = new Transaction
//            {
//                Amount = order.TotalAmount,
//                CreateDate = DateTime.Now,
//                Type = TransactionConstant.TRANSACTION_TYPE_DEPOSIT,
//                RelatedId = order.OrderId,
//                UserId = order.UserId,
//                Description = $"Thanh toán bằng ví cho Order có ID {order.OrderId} với số tiền {order.TotalAmount}"
//            };

//            await _uow.GetRepository<Transaction>().InsertAsync(transaction);
//        }

//        private async Task UpdateWalletUserAsync(VnPaymentResponseModel response)
//        {
//            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
//                        predicate: p => p.UserId == response.RelateId
//                    );
//            user.WalletBalance += response.Amount;
//            _uow.GetRepository<User>().UpdateAsync(user);
//        }        

//        public string GetRedirectUrl()
//        {
//            return _config.RedirectUrl;            
//        }
//    }
//}
