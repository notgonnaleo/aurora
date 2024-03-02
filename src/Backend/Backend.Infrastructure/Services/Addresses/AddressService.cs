using Backend.Domain.Entities.Addresses;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Addresses
{
    public class AddressService : Service
    {
        private readonly AppDbContext _appDbContext;
        public AddressService(AppDbContext appDbContext, UserContextService main) : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Address> GetAddresses(Guid tenantId)
        {
            return _appDbContext.Addresses
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Address? GetAddressById(Guid tenantId, Guid addressId)
        {
            return _appDbContext.Addresses
                .Where(x => x.TenantId == tenantId && x.AddressId == addressId)
                .FirstOrDefault();
        }

        public Address AddAddress(Address address)
        {
            var context = LoadContext();
            address = new Address(address, context.UserId);
            address.ValidateFields(context.Language);

            _appDbContext.Addresses.Add(address);
            if (_appDbContext.SaveChanges() > 0)
                return address;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool UpdateAddress(Address address)
        {
            var context = LoadContext();
            address.Updated = DateTime.UtcNow;
            address.UpdatedBy = context.UserId;
            address.ValidateFields(context.Language);

            _appDbContext.Update(address);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool DeleteAddres(Guid tenantId, Guid addressId)
        {
            var context = LoadContext();
            var address = _appDbContext.Addresses
                .Where(x => x.AddressId == addressId && x.TenantId == tenantId)
                .FirstOrDefault();

            if (address != null)
            {
                address.Active = false;
                _appDbContext.Update(address);
                return _appDbContext.SaveChanges() > 0;
            }

            return false;
        }
    }
}
