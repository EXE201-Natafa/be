using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.UserModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<MethodResult<User>> GetByEmailAsync(string email);
        Task<MethodResult<ProfileResponse>> GetMyProfileAsync(string email);
        Task<MethodResult<ProfileResponse>> GetProfileByUserIdAsync(int userId);
        Task<MethodResult<string>> UpdateProfileAsync(string email, UpdateProfileRequest request);
        Task<MethodResult<string>> CreateUserAsync(UserRequest request);
        Task<MethodResult<string>> UpdateUserAsync(int id, UserRequest request);
        Task<MethodResult<string>> UpdateUserStatusAsync(int id, UpdateStatusRequest request);
        Task<MethodResult<IPaginate<UserResponse>>> GetAllUsersAsync(PaginateRequest request);
        Task<MethodResult<IPaginate<UserResponse>>> GetUsersByVoucherIdAsync(int voucherId, PaginateRequest request);

    }
}
