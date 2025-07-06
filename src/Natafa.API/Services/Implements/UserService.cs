using AutoMapper;
using Natafa.Api.Helper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Models.UserModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Models;

namespace Natafa.Api.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICloudinaryService _cloudinaryService;

        public UserService(IUnitOfWork uow, IMapper mapper, IEmailService emailService, ICloudinaryService cloudinaryService)
        {
            _uow = uow;
            _mapper = mapper;
            _emailService = emailService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<MethodResult<User>> GetByEmailAsync(string email)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: p => p.Email == email
                );
            if (user == null)
            {
                return new MethodResult<User>.Failure("User not found", StatusCodes.Status404NotFound);
            }
            return new MethodResult<User>.Success(user);
        }

        public async Task<MethodResult<ProfileResponse>> GetMyProfileAsync(string email)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: p => p.Email == email
                );

            var result = _mapper.Map<ProfileResponse>(user);
            return new MethodResult<ProfileResponse>.Success(result);
        }

        public async Task<MethodResult<ProfileResponse>> GetProfileByUserIdAsync(int userId)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: p => p.UserId == userId
                );

            var result = _mapper.Map<ProfileResponse>(user);
            return new MethodResult<ProfileResponse>.Success(result);
        }

        public async Task<MethodResult<string>> UpdateProfileAsync(string email, UpdateProfileRequest request)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: p => p.Email == email
                );
            if (user == null)
            {
                return new MethodResult<string>.Failure("User not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(request, user);
            if (request.Image != null)
            {
                // Xử lý ảnh đại diện
                var image = await _cloudinaryService.UploadImageAsync(request.Image);
                if (image == null)
                {
                    return new MethodResult<string>.Failure("Failed to upload avatar", StatusCodes.Status500InternalServerError);
                }
                user.Image = image;
            }
            _uow.GetRepository<User>().UpdateAsync(user);
            _uow.Commit();
            return new MethodResult<string>.Success("Update profile successfully");
        }

        public async Task<MethodResult<string>> DisableUserAsync()
        {

            return new MethodResult<string>.Success("Update profile successfully");
        }

        public async Task<MethodResult<string>> CreateUserAsync(UserRequest request)
        {
            try
            {
                // Ánh xạ từ request sang entity
                var user = _mapper.Map<User>(request);

                await _uow.GetRepository<User>().InsertAsync(user);
                await _uow.CommitAsync();

                // Ánh xạ từ entity sang response
                var response = _mapper.Map<string>(user);
                return new MethodResult<string>.Success("Create user successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        // Cập nhật hồ sơ cá nhân


        // Cập nhật thông tin người dùng
        public async Task<MethodResult<string>> UpdateUserAsync(int id, UserRequest request)
        {
            try
            {
                // Sử dụng overload cho thực thể đầy đủ
                var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: u => u.UserId == id
                );

                if (user == null)
                    return new MethodResult<string>.Failure("User not found", StatusCodes.Status404NotFound);

                // Ánh xạ từ request sang user
                _mapper.Map(request, user);

                _uow.GetRepository<User>().UpdateAsync(user);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("User updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        // Cập nhật trạng thái người dùng
        public async Task<MethodResult<string>> UpdateUserStatusAsync(int id, UpdateStatusRequest request)
        {
            try
            {
                // Chọn đúng overload để chỉ lấy thực thể
                var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: u => u.UserId == id
                );

                if (user == null)
                    return new MethodResult<string>.Failure("User not found", StatusCodes.Status404NotFound);

                // Cập nhật trạng thái
                user.Status = request.Status;

                _uow.GetRepository<User>().UpdateAsync(user);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("User status updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<IPaginate<UserResponse>>> GetAllUsersAsync(PaginateRequest request)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<User, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.FullName.Contains(search) ||
                                                 p.Email.Contains(search)) &&
                (string.IsNullOrEmpty(filter) || filter.Equals(p.Role.ToLower()));

            var users = await _uow.GetRepository<User>().GetPagingListAsync(
                selector: s => _mapper.Map<UserResponse>(s),
                predicate: predicate,
                orderBy: BuildOrderBy(request.sortBy),
                page: page,
                size: size
            );

            return new MethodResult<IPaginate<UserResponse>>.Success(users);
        }

        public async Task<MethodResult<IPaginate<UserResponse>>> GetUsersByVoucherIdAsync(int voucherId, PaginateRequest request)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<User, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.FullName.Contains(search) ||
                                                 p.Email.Contains(search)) &&
                (string.IsNullOrEmpty(filter) || filter.Equals(p.Role.ToLower())) &&
                p.UserVouchers.Any(x => x.VoucherId == voucherId);

            var users = await _uow.GetRepository<User>().GetPagingListAsync(
                selector: s => _mapper.Map<UserResponse>(s),
                predicate: predicate,
                orderBy: BuildOrderBy(request.sortBy),
                page: page,
                size: size
            );

            return new MethodResult<IPaginate<UserResponse>>.Success(users);
        }

        private Func<IQueryable<User>, IOrderedQueryable<User>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return null;

            return sortBy.ToLower() switch
            {
                //"wallet" => q => q.OrderBy(p => p.WalletBalance),
                //"wallet_desc" => q => q.OrderByDescending(p => p.WalletBalance),
                "date" => q => q.OrderBy(p => p.Birthday),
                "date_desc" => q => q.OrderByDescending(p => p.Birthday),
                _ => q => q.OrderByDescending(p => p.UserId) // Default sort
            };
        }   
    }
}
