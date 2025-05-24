using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.FeedbackModel;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<MethodResult<bool>> CheckAbilityFeedback(int userId, int productId);
        Task<MethodResult<string>> CreateFeedbackAsync(int userId, FeedbackRequest request);
        Task<MethodResult<IPaginate<FeedbackResponse>>> GetFeedbackByProductAsync(int productId, PaginateRequest request, int? rating);

        Task<MethodResult<IPaginate<FeedbackResponse>>> GetAllFeedbacksAsync(PaginateRequest request, int? rating);
        Task<MethodResult<string>> UpdateFeedbackStatusAsync(int feedbackId, UpdateFeedbackStatusRequest request);
        Task<MethodResult<string>> DeleteFeedbackByIdAsync(int userId, int id);
    }
}
