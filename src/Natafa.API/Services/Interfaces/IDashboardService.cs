using Natafa.Api.Helper;
using Natafa.Api.ViewModels;

namespace Natafa.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<MethodResult<DashboardResponse>> GetDashboardAsync();
        Task<MethodResult<IEnumerable<TotalFeedbacksByMonthResponse>>> GetTotalFeedbacksByMonthAsync();
        Task<MethodResult<IEnumerable<TotalFeedbacksByQuarterResponse>>> GetTotalFeedbacksByQuarterAsync();
        Task<MethodResult<IEnumerable<TotalFeedbacksByYearResponse>>> GetTotalFeedbacksByYearAsync();
    }
}
