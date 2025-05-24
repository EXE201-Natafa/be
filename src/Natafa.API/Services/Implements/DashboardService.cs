using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Natafa.Api.Services.Implements
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MethodResult<DashboardResponse>> GetDashboardAsync()
        {
            try
            {                
                var products = await _uow.GetRepository<Product>().GetListAsync();
                var orders = await _uow.GetRepository<Order>().GetListAsync(
                     include: i => i.Include(p => p.OrderTrackings)
                    );
                var users = await _uow.GetRepository<User>().GetListAsync();
                var result = new DashboardResponse
                {
                    TotalProducts = products.Count,
                    TotalOrders = orders.Count,
                    CompletedOrders = orders.Count(o => o.OrderTrackings.OrderByDescending(os => os.UpdatedDate).FirstOrDefault().Status == OrderConstant.ORDER_STATUS_COMPLETED),
                    TotalUsers = users.Count(x => x.Role == UserConstant.USER_ROLE_CUSTOMER),
                };
                return new MethodResult<DashboardResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return new MethodResult<DashboardResponse>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<IEnumerable<TotalFeedbacksByMonthResponse>>> GetTotalFeedbacksByMonthAsync()
        {
            var feedbacks = await _uow.GetRepository<Feedback>().GetListAsync(
                selector: s => s.CreatedDate
            );

            var result = feedbacks.AsEnumerable().GroupBy(x => new { x.Year, x.Month })
                            .Select(o => new TotalFeedbacksByMonthResponse
                            {
                                Month = o.Key.Month,
                                Year = o.Key.Year,
                                TotalFeedbacks = o.Count(),
                            })
                            .OrderBy(x => x.Year)
                            .ThenBy(x => x.Month)
                            .ToList();
            return new MethodResult<IEnumerable<TotalFeedbacksByMonthResponse>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<TotalFeedbacksByQuarterResponse>>> GetTotalFeedbacksByQuarterAsync()
        {
            var feedbacks = await _uow.GetRepository<Feedback>().GetListAsync(
                selector: s => s.CreatedDate
            );

            var result = feedbacks.GroupBy(x => new
            {
                x.Year,
                Quarter = (x.Month - 1) / 3 + 1
            })
                            .Select(o => new TotalFeedbacksByQuarterResponse
                            {
                                Quarter = o.Key.Quarter,
                                Year = o.Key.Year,
                                TotalFeedbacks = o.Count(),
                            })
                            .OrderBy(x => x.Year)
                            .ThenBy(x => x.Quarter)
                            .ToList();
            return new MethodResult<IEnumerable<TotalFeedbacksByQuarterResponse>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<TotalFeedbacksByYearResponse>>> GetTotalFeedbacksByYearAsync()
        {
            var feedbacks = await _uow.GetRepository<Feedback>().GetListAsync(
                selector: s => s.CreatedDate
            );

            var result = feedbacks.GroupBy(x => x.Year)
                            .Select(o => new TotalFeedbacksByYearResponse
                            {
                                Year = o.Key,
                                TotalFeedbacks = o.Count(),
                            })
                            .OrderBy(x => x.Year)
                            .ToList();
            return new MethodResult<IEnumerable<TotalFeedbacksByYearResponse>>.Success(result);
        }


    }
}
