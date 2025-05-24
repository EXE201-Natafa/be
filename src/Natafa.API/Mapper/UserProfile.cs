using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Models.UserModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Microsoft.VisualBasic;
using System.Security.Principal;

namespace Natafa.Api.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignupRequest, User>()
                  .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                  .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserConstant.USER_ROLE_CUSTOMER))
                  .ForMember(dest => dest.ConfirmedEmail, opt => opt.MapFrom(src => UserConstant.USER_CONFIRMED_EMAIL_INACTIVE))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => UserConstant.USER_STATUS_ACTIVE));
            CreateMap<User, ProfileResponse>();
            CreateMap<UpdateProfileRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
