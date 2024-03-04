using Backend.Domain.Entities.Profiles;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Profiles
{
    public class ProfileService : Service
    {
        private readonly AppDbContext _appDbContext;

        public ProfileService(AppDbContext appDbContext, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Profile> GetProfiles(Guid tenantId)
        {
            return _appDbContext.Profiles
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Profile? GetProfileById(Guid tenantId, Guid profileId)
        {
            return _appDbContext.Profiles
                .Where(x => x.TenantId == tenantId && x.ProfileId == profileId)
                .FirstOrDefault();
        }

        public Profile AddProfile(Profile profile)
        {
            var context = LoadContext();
            profile = new Profile(profile, context.UserId);
            profile.ValidateFields(context.Language);

            _appDbContext.Profiles.Add(profile);
            if (_appDbContext.SaveChanges() > 0)
                return profile;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool UpdateProfile(Profile profile)
        {
            var context = LoadContext();
            profile.ValidateFields(context.Language);

            _appDbContext.Update(profile);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool DeleteProfile(Guid tenantId, Guid profileId)
        {
            var context = LoadContext();
            var profile = _appDbContext.Profiles
                .Where(x => x.ProfileId == profileId && x.TenantId == tenantId)
                .FirstOrDefault();

            if (profile != null)
            {
                _appDbContext.Remove(profile);
                return _appDbContext.SaveChanges() > 0;
            }

            return false;
        }
    }
}
