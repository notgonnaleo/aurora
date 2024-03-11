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
    public class EmailAddressService : Service
    {
        private readonly AppDbContext _appDbContext;

        public EmailAddressService(AppDbContext appDbContext, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Email> GetEmails(Guid tenantId)
        {
            return _appDbContext.Emails
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Email? GetEmailById(Guid tenantId, Guid emailAddressId)
        {
            return _appDbContext.Emails
                .Where(x => x.TenantId == tenantId && x.EmailAddressId == emailAddressId)
                .FirstOrDefault();
        }

        public Email AddEmail(Email email)
        {
            var context = LoadContext();
            email = new Email(email, context.UserId);
            email.ValidateFields(context.Language);

            _appDbContext.Emails.Add(email);
            _appDbContext.SaveChanges();
            return email;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool UpdateEmail(Email email)
        {
            var context = LoadContext();
            email.Updated = DateTime.UtcNow;
            email.UpdatedBy = context.UserId;
            email.ValidateFields(context.Language);

            _appDbContext.Update(email);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool DeleteEmail(Guid tenantId, Guid emailAddressId)
        {
            var context = LoadContext();
            var email = _appDbContext.Emails
                .Where(x => x.EmailAddressId == emailAddressId && x.TenantId == tenantId)
                .FirstOrDefault();

            if (email != null)
            {
                email.Active = false;
                _appDbContext.Update(email);
                return _appDbContext.SaveChanges() > 0;
            }

            return false;
        }
    }
}
