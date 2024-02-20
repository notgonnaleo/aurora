using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.SubCategories
{
    public class SubCategoryService : Service
    {
        private readonly AppDbContext _appDbContext;

        public SubCategoryService(AppDbContext appDbContext, UserContextService userContextService) : base(userContextService)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<SubCategory> Get()
        {
            var context = LoadContext();
            return _appDbContext.SubCategories
                .Where(x => x.TenantId == context.Tenant.Id);
        }

        public SubCategory GetById(Guid subcategoryId)
        {
            var context = LoadContext();
            return _appDbContext.SubCategories
                .First(x => x.TenantId == context.Tenant.Id && x.SubCategoryId == subcategoryId);

        }
        public IEnumerable<SubCategory> GetSubCategoriesByCategory(Guid categoryId)
        {
            var context = LoadContext();
            return _appDbContext.SubCategories
                .Where(x => x.TenantId == context.Tenant.Id && x.CategoryId == categoryId && x.Active);
        }

        public SubCategory Add(SubCategory subCategory)
        {
            var context = LoadContext();
            subCategory.SubCategoryId = Guid.NewGuid();
            subCategory.TenantId = context.Tenant.Id;
            subCategory.CreatedBy = context.UserId;
            subCategory.Created = DateTime.UtcNow;
            subCategory.Updated = null;
            subCategory.UpdatedBy = null;
            subCategory.Active = true;
            subCategory.ValidateFields(context.Language);

            _appDbContext.SubCategories.Add(subCategory);
            if (_appDbContext.SaveChanges() > 0)
                return subCategory;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool Update(SubCategory subCategory)
        {
            var context = LoadContext();
            ValidateTenant(subCategory.TenantId);
            subCategory.SubCategoryName = subCategory.SubCategoryName;
            subCategory.Updated = DateTime.UtcNow;
            subCategory.UpdatedBy = context.UserId;
            subCategory.ValidateFields(context.Language);

            _appDbContext.Update(subCategory);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid subcategoryId)
        {
            var context = LoadContext();
            SubCategory subCategory = _appDbContext.SubCategories
                .Where(x => x.TenantId == context.Tenant.Id && x.SubCategoryId == subcategoryId)
                .First();
            subCategory.Active = false;
            _appDbContext.Update(subCategory);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}

