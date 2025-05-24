using AutoMapper;
using Natafa.Api.Models.VoucherModel;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<Voucher, VoucherResponse>();
            CreateMap<VoucherRequest, Voucher>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
        }
    }

}
