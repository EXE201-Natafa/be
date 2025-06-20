using AutoMapper;
using Natafa.Api.Models.ShippingAddressModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Mapper
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            CreateMap<ShippingAddressRequest, ShippingAddress>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressResponse>();
        }
    }
}
