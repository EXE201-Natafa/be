using AutoMapper;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodResponse>();
        }
    }
}
