using Backend.Domain.Entities.Contacts;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Contact
{
    public class PhoneService : Service
    {
        private readonly AppDbContext _appDbContext;

        public PhoneService(AppDbContext appDbContext, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Phone> GetPhones(Guid tenantId)
        {
            return _appDbContext.Phones
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Phone? GetPhoneById(Guid tenantId, Guid phoneId)
        {
            return _appDbContext.Phones
                .Where(x => x.TenantId == tenantId && x.PhoneId == phoneId)
                .FirstOrDefault();
        }

        public async Task<Phone> AddPhone(Phone phone)
        {
            var context = LoadContext();
            phone = new Phone(phone, context.UserId);
            phone.ValidateFields(context.Language);

            _appDbContext.Phones.Add(phone);
            if(await _appDbContext.SaveChangesAsync() > 0)
             return phone;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool UpdatePhone(Phone phone)
        {
            var context = LoadContext();
            phone.Updated = DateTime.UtcNow;
            phone.UpdatedBy = context.UserId;
            phone.ValidateFields(context.Language);

            _appDbContext.Update(phone);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool DeletePhone(Guid tenantId, Guid phoneId)
        {
            var context = LoadContext();
            var phone = _appDbContext.Phones
                .Where(x => x.PhoneId == phoneId && x.TenantId == tenantId)
                .FirstOrDefault();

            if (phone != null)
            {
                phone.Active = false;
                _appDbContext.Update(phone);
                return _appDbContext.SaveChanges() > 0;
            }

            return false;
        }
    }
}
