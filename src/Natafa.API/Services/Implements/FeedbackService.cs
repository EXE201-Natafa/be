using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Models.FeedbackModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Org.BouncyCastle.Asn1.Ocsp;
using Natafa.Api.Models;

namespace Natafa.Api.Services.Implements
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public FeedbackService(IUnitOfWork uow, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _uow = uow;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
       
        public async Task<MethodResult<string>> CreateFeedbackAsync(int userId, FeedbackRequest request)
        {
            try
            {
                var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(
                    predicate: p => p.ProductId == request.ProductId
                );

                if (product == null)
                {
                    return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
                }

                var feedback = _mapper.Map<Feedback>(request);
                feedback.UserId = userId;

                // Upload ảnh lên Cloudinary nếu có
                if (request.Images != null && request.Images.Any())
                {
                    var imageUrls = await _cloudinaryService.UploadMultipleImagesAsync(request.Images);
                    if (!imageUrls.Any())
                    {
                        return new MethodResult<string>.Failure("Fail while upload image(s)", StatusCodes.Status500InternalServerError);
                    }

                    foreach (var url in imageUrls)
                    {
                        feedback.FeedbackImages.Add(new FeedbackImage
                        {
                            Url = url,
                        });
                    }
                }               

                await _uow.GetRepository<Feedback>().InsertAsync(feedback);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Feedback created successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<bool>> CheckAbilityFeedback(int userId, int productId)
        {
            try
            {
                var feedbacks = await _uow.GetRepository<Feedback>().GetListAsync(
                    predicate: f => f.UserId == userId && f.ProductId == productId
                );

                var orders = await _uow.GetRepository<Order>().GetListAsync(
                    predicate: o => o.OrderDetails.Any(x => x.ProductDetail.ProductId == productId) && o.UserId == userId,
                    include: i => i.Include(x => x.OrderDetails).ThenInclude(x => x.ProductDetail)
                );

                if (feedbacks.Count < orders.Count)
                {
                    return new MethodResult<bool>.Success(true);
                }

                return new MethodResult<bool>.Success(false);
            }
            catch (Exception e)
            {
                return new MethodResult<bool>.Failure(e.Message, StatusCodes.Status500InternalServerError);
            }
            
        }

        // Lấy danh sách phản hồi theo sản phẩm
        public async Task<MethodResult<IPaginate<FeedbackResponse>>> GetFeedbackByProductAsync(int productId, PaginateRequest request, int? rating)
        {
            try
            {
                int page = request.page > 0 ? request.page : 1;
                int size = request.size > 0 ? request.size : 10;
                string search = request.search?.ToLower() ?? string.Empty;
                string filter = request.filter?.ToLower() ?? string.Empty;

                Expression<Func<Feedback, bool>> predicate = p =>
                        (string.IsNullOrEmpty(search) || p.Comment.Contains(search)) &&
                        (string.IsNullOrEmpty(filter) || 
                            (filter.Contains("active") && p.Status) ||
                            (filter.Contains("inactive") && !p.Status)) &&  
                        (rating == null || p.Rating == rating) &&
                        p.ProductId == productId;

                var feedbacks = await _uow.GetRepository<Feedback>().GetPagingListAsync(
                    selector: f => _mapper.Map<FeedbackResponse>(f),
                    predicate: predicate,
                    include: f => f.Include(x => x.FeedbackImages),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );

                return new MethodResult<IPaginate<FeedbackResponse>>.Success(feedbacks);
            }
            catch (Exception ex)
            {
                return new MethodResult<IPaginate<FeedbackResponse>>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        // Lấy tất cả phản hồi
        public async Task<MethodResult<IPaginate<FeedbackResponse>>> GetAllFeedbacksAsync(PaginateRequest request, int? rating)
        {
            try
            {
                int page = request.page > 0 ? request.page : 1;
                int size = request.size > 0 ? request.size : 10;
                string search = request.search?.ToLower() ?? string.Empty;
                string filter = request.filter?.ToLower() ?? string.Empty;

                Expression<Func<Feedback, bool>> predicate = p =>
                        (string.IsNullOrEmpty(search) || p.Comment.Contains(search)) &&
                        (string.IsNullOrEmpty(filter) ||
                            (filter.Contains("active") && p.Status) ||
                            (filter.Contains("inactive") && !p.Status)) &&
                        (rating == null || p.Rating == rating);

                var feedbacks = await _uow.GetRepository<Feedback>().GetPagingListAsync(
                    selector: f => _mapper.Map<FeedbackResponse>(f),
                    predicate: predicate,
                    include: f => f.Include(x => x.FeedbackImages),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );

                return new MethodResult<IPaginate<FeedbackResponse>>.Success(feedbacks);
            }
            catch (Exception ex)
            {
                return new MethodResult<IPaginate<FeedbackResponse>>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private Func<IQueryable<Feedback>, IOrderedQueryable<Feedback>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return null;

            return sortBy.ToLower() switch
            {
                "rating" => q => q.OrderBy(p => p.Rating),
                "rating_desc" => q => q.OrderByDescending(p => p.Rating),
                "date" => q => q.OrderBy(p => p.CreatedDate),
                "date_desc" => q => q.OrderByDescending(p => p.CreatedDate),
                _ => q => q.OrderByDescending(p => p.FeedbackId) // Default sort
            };
        }

        // Cập nhật trạng thái phản hồi
        public async Task<MethodResult<string>> UpdateFeedbackStatusAsync(int feedbackId, UpdateFeedbackStatusRequest request)
        {
            try
            {
                var feedback = await _uow.GetRepository<Feedback>().SingleOrDefaultAsync(
                    predicate: f => f.FeedbackId == feedbackId
                );

                if (feedback == null)
                {
                    return new MethodResult<string>.Failure("Feedback not found", StatusCodes.Status404NotFound);
                }

                feedback.Status = request.Status;
                _uow.GetRepository<Feedback>().UpdateAsync(feedback);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Feedback updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> DeleteFeedbackByIdAsync(int userId, int id)
        {
            try
            {
                var feedback = await _uow.GetRepository<Feedback>().SingleOrDefaultAsync(
                    predicate: f => f.FeedbackId == id
                );

                if (feedback == null)
                {
                    return new MethodResult<string>.Failure("Feedback not found", StatusCodes.Status404NotFound);
                }

                var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: u => u.UserId == userId
                );

                if (user.Role == UserConstant.USER_ROLE_CUSTOMER)
                {
                    if (userId != feedback.UserId)
                    {
                        return new MethodResult<string>.Failure("You can not delete this feedback", StatusCodes.Status403Forbidden);
                    }
                }

                _uow.GetRepository<Feedback>().DeleteAsync(feedback);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Feedback deleted successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
