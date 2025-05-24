using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;

namespace Natafa.Api.Services.Implements
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ShippingService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<decimal> GetShippingCostAsync(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            decimal weight = 0;
            foreach (var item in request)
            {
                var productDetail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(
                        predicate: p => p.ProductDetailId == item.ProductDetailId
                );               
                weight += productDetail.Weight * item.Quantity;
            }

            var priceEnoughWeight = await _uow.GetRepository<ShippingPriceTable>().SingleOrDefaultAsync(
                        predicate: p => (decimal) p.FromWeight < weight && (decimal)p.ToWeight > weight
                );       
            if (priceEnoughWeight != null)
            {
                if (inRegion)
                {
                    return priceEnoughWeight.InRegion;
                }
                else
                {
                    return priceEnoughWeight.OutRegion;
                }
            }
            else
            {
                var priceOverWeight = await _uow.GetRepository<ShippingPriceTable>().SingleOrDefaultAsync(
                        orderBy: o => o.OrderByDescending(x => x.FromWeight)
                );
                if (inRegion)
                {
                    return priceOverWeight.InRegion + (decimal)(Math.Ceiling(weight - (decimal)priceOverWeight.FromWeight) * (decimal)priceOverWeight.Pir);
                }
                else
                {
                    return priceOverWeight.OutRegion + (decimal)(Math.Ceiling(weight - (decimal)priceOverWeight.FromWeight) * (decimal)priceOverWeight.Por);
                }
            }
        }
    }
}
