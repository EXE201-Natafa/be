using AutoMapper;
using Microsoft.AspNetCore.Http;
using Natafa.Api.Helper;
using Natafa.Api.Models.ShippingAddressModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Repository.Implement;
using Natafa.Repository.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Implements
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ShippingAddressService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<MethodResult<ShippingAddressResponse>> GetOneByIdAsync(int shippingAddressId)
        {
            var result = await _uow.GetRepository<ShippingAddress>().SingleOrDefaultAsync(
                selector: s => _mapper.Map<ShippingAddressResponse>(s),
                predicate: p => p.ShippingAddressId == shippingAddressId
            );
            return new MethodResult<ShippingAddressResponse>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<ShippingAddressResponse>>> GetAllByUserIdAsync(int userId)
        {
            var result = await _uow.GetRepository<ShippingAddress>().GetListAsync(
                selector: s => _mapper.Map<ShippingAddressResponse>(s),
                predicate: p => p.UserId == userId
            );
            return new MethodResult<IEnumerable<ShippingAddressResponse>>.Success(result);
        }

        public async Task<MethodResult<string>> CreateOneAsync(int userId, ShippingAddressRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var shipAdd = _mapper.Map<ShippingAddress>(request);

                var ShipAddDbs = await _uow.GetRepository<ShippingAddress>().GetListAsync(
                    predicate: p => p.UserId == userId
                );

                if (ShipAddDbs == null || ShipAddDbs.Count == 0)
                {
                    shipAdd.IsDefault = true;
                }
                else
                {
                    if (request.IsDefault == true)
                    {
                        var shipAddDefault = await _uow.GetRepository<ShippingAddress>().SingleOrDefaultAsync(
                            predicate: p => p.UserId == userId && p.IsDefault == true
                        );
                        if (shipAddDefault != null)
                        {
                            shipAddDefault.IsDefault = false;
                            _uow.GetRepository<ShippingAddress>().UpdateAsync(shipAddDefault);
                        }
                    }
                }

                shipAdd.UserId = userId;

                await _uow.GetRepository<ShippingAddress>().InsertAsync(shipAdd);
                await _uow.CommitAsync();
                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Create shipping address succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> UpdateOneAsync(int userId, int shipAddId, ShippingAddressRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var shipAdd = await _uow.GetRepository<ShippingAddress>().SingleOrDefaultAsync(
                    predicate: p => p.ShippingAddressId == shipAddId
                );
                if (shipAdd == null)
                {
                    return new MethodResult<string>.Failure("Shipping address not found", StatusCodes.Status404NotFound);
                }
                if (shipAdd.UserId != userId)
                {
                    return new MethodResult<string>.Failure("you do not have this shipping address", StatusCodes.Status400BadRequest);
                }

                if (request.IsDefault == true)
                {
                    var shipAddDefault = await _uow.GetRepository<ShippingAddress>().SingleOrDefaultAsync(
                        predicate: p => p.UserId == userId && p.IsDefault == true && p.ShippingAddressId != shipAddId
                    );
                    if (shipAddDefault != null)
                    {
                        shipAddDefault.IsDefault = false;
                        _uow.GetRepository<ShippingAddress>().UpdateAsync(shipAddDefault);
                    }
                }

                _mapper.Map(request, shipAdd);
                _uow.GetRepository<ShippingAddress>().UpdateAsync(shipAdd);
                await _uow.CommitAsync();
                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Update shipping address succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }            
        }
        
        public async Task<MethodResult<string>> DeleteOneAsync(int userId, int shipAddId)
        {
            try
            {
                var shipAdd = await _uow.GetRepository<ShippingAddress>().SingleOrDefaultAsync(
                    predicate: p => p.ShippingAddressId == shipAddId
                );
                if (shipAdd == null)
                {
                    return new MethodResult<string>.Failure("Shipping address not found", StatusCodes.Status404NotFound);
                }
                if (shipAdd.UserId != userId)
                {
                    return new MethodResult<string>.Failure("you do not have this shipping address", StatusCodes.Status400BadRequest);
                }

                _uow.GetRepository<ShippingAddress>().DeleteAsync(shipAdd);
                await _uow.CommitAsync();
                return new MethodResult<string>.Success("Delete shipping address succesfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
