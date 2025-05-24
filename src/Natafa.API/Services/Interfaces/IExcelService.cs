using Natafa.Api.ViewModels;

namespace Natafa.Api.Services.Interfaces
{
    public interface IExcelService
    {
        Task<IEnumerable<RevenueByMonthResponse>> GetRevenueByMonthAsync();
        Task<IEnumerable<RevenueByQuarterResponse>> GetRevenueByQuarterAsync();
        Task<IEnumerable<RevenueByYearResponse>> GetRevenueByYearAsync();
    }
}
