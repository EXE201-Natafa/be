using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Natafa.Api.Services.Implements
{
    public class ExcelService : IExcelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExcelService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RevenueByMonthResponse>> GetRevenueByMonthAsync()
        {
            var orders = await _uow.GetRepository<Order>().GetListAsync(
                    selector: s => new { s.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().UpdatedDate, s.TotalAmount },
                    predicate: p => p.OrderTrackings.Any(x => x.Status == OrderConstant.ORDER_STATUS_COMPLETED),
                    include: i => i.Include(x => x.OrderTrackings)
                );

            return orders.GroupBy(x => new { x.UpdatedDate.Year, x.UpdatedDate.Month })
                         .Select(o => new RevenueByMonthResponse
                         {
                             Month = o.Key.Month,
                             Year = o.Key.Year,
                             Amount = o.Sum(x => x.TotalAmount),
                         })
                         .OrderBy(x => x.Year)
                         .ThenBy(x => x.Month)
                         .ToList();
        }

        public async Task<IEnumerable<RevenueByQuarterResponse>> GetRevenueByQuarterAsync()
        {
            var orders = await _uow.GetRepository<Order>().GetListAsync(
                    selector: s => new { s.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().UpdatedDate, s.TotalAmount },
                    predicate: p => p.OrderTrackings.Any(x => x.Status == OrderConstant.ORDER_STATUS_COMPLETED),
                    include: i => i.Include(x => x.OrderTrackings)
                );

            return orders.GroupBy(x => new
            {
                x.UpdatedDate.Year,
                Quarter = (x.UpdatedDate.Month - 1) / 3 + 1
            })
                         .Select(o => new RevenueByQuarterResponse
                         {
                             Quarter = o.Key.Quarter,
                             Year = o.Key.Year,
                             Amount = o.Sum(x => x.TotalAmount),
                         })
                         .OrderBy(x => x.Year)
                         .ThenBy(x => x.Quarter)
                         .ToList();
        }

        public async Task<IEnumerable<RevenueByYearResponse>> GetRevenueByYearAsync()
        {
            var orders = await _uow.GetRepository<Order>().GetListAsync(
                    selector: s => new { s.OrderTrackings.OrderByDescending(x => x.UpdatedDate).FirstOrDefault().UpdatedDate, s.TotalAmount },
                    predicate: p => p.OrderTrackings.Any(x => x.Status == OrderConstant.ORDER_STATUS_COMPLETED),
                    include: i => i.Include(x => x.OrderTrackings)
                );

            return orders.GroupBy(x => x.UpdatedDate.Year)
                         .Select(o => new RevenueByYearResponse
                         {
                             Year = o.Key,
                             Amount = o.Sum(x => x.TotalAmount),
                         })
                         .OrderBy(x => x.Year)
                         .ToList();
        }
    }
}
