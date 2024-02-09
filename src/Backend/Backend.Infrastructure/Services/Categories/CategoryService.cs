using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.SubCategories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Categories
{
    public class CategoryService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly SubCategoryService _subCategoryService;

        public CategoryService(AppDbContext appDbContext, UserContextService userContextService, SubCategoryService subCategoryService) : base(userContextService)
        {
            _appDbContext = appDbContext;
            _subCategoryService = subCategoryService;
        }

        public IEnumerable<Category> Get()
        {
            var context = LoadContext();
            return _appDbContext.Categories
                .Where(x => x.TenantId == context.Tenant.Id)
                .ToList();
        }

        public Category GetById(Guid categoryId)
        {
            var context = LoadContext();
            var res = _appDbContext.Categories.FirstOrDefault(x => x.TenantId == context.Tenant.Id && x.CategoryId == categoryId) ?? throw new Exception(Localization.GenericValidations.ErrorItemNotFound(context.Language));
            return res;
        }

        public Category Add(Category category)
        {
            var context = LoadContext();
            category.TenantId = context.Tenant.Id;
            category.CategoryId = Guid.NewGuid();
            category.CreatedBy = context.UserId;
            category.Created = DateTime.UtcNow;
            category.Updated = null;
            category.UpdatedBy = null;
            category.Active = true;
            category.ValidateFields(context.Language);

            _appDbContext.Categories.Add(category);
            if (_appDbContext.SaveChanges() > 0)
                return category;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool Update(Category category)
        {
            var context = LoadContext();
            ValidateTenant(category.TenantId);
            category.Updated = DateTime.UtcNow;
            category.UpdatedBy = context.UserId;
            category.Active = true;
            category.ValidateFields(context.Language);

            _appDbContext.Update(category);
            return _appDbContext.SaveChanges() > 0;
        }

        public IEnumerable<Category> GetCategoryAndSubCategories()
        {
            var context = LoadContext();
            List<Category> categories = _appDbContext.Categories
                .Where(x => x.TenantId == context.Tenant.Id && x.Active).ToList();

            if (categories is not null && categories.Count() > 0)
            {
                foreach (var category in categories)
                {
                    category.SubCategories = _subCategoryService.GetSubCategoriesByCategory(category.CategoryId); // TODO: Make this more efficient, like getting it all in one query, you can use joins in entity framework
                }
                return categories;
            }
            return new List<Category>();
        }

        public bool Delete(Guid categoryId)
        {
            var context = LoadContext();
            Category category = _appDbContext.Categories
                .Where(x => x.TenantId == context.Tenant.Id && x.CategoryId == categoryId)
                .First();
            category.Active = false;
            _appDbContext.Update(category);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
