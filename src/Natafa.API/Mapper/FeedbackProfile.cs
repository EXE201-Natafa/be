namespace Natafa.Api.Mapper
{
    using AutoMapper;
    using Natafa.Api.Constants;
    using Natafa.Api.Models.FeedbackModel;
    using Natafa.Domain.Entities;

    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {      
            CreateMap<Feedback, FeedbackResponse>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FeedbackImages.Select(f => f.Url)));          
            CreateMap<FeedbackRequest, Feedback>();
            CreateMap<User, UserFeedback>();

        }
    }

}
