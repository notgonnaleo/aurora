using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Context;
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

        public IEnumerable<Category> Get(Guid tenantId)
        {
            return _appDbContext.Categories
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Category GetById(Guid categoryId, Guid tenantId)
        {
            return _appDbContext.Categories
                .FirstOrDefault(x => x.TenantId == tenantId && x.CategoryId == categoryId);
        }

        public async Task<Category> Add(Category category)
        {
            var context = LoadContext();
            category.CategoryId = Guid.NewGuid();
            category.CreatedBy = context.UserId;
            category.Created = DateTime.Now;
            category.Updated = null;
            category.UpdatedBy = null;
            category.Active = true;
            _appDbContext.Categories.Add(category);
            if(await _appDbContext.SaveChangesAsync() > 0)
                return category;

            throw new Exception("Failed while trying to save new category.");
        }

        public async Task<bool> Update(Category category)
        {
            var context = LoadContext();
            category.TenantId = context.Tenant.Id;
            category.Updated = DateTime.UtcNow;
            category.UpdatedBy = context.UserId;
            category.Active = true;

            _appDbContext.Update(category);
            return _appDbContext.SaveChanges() > 0;
        }

        public async Task<IEnumerable<Category>> GetCategoryAndSubCategories(Guid tenantId)
        {
            List<Category> categories = _appDbContext.Categories
                .Where(x => x.TenantId == tenantId && x.Active).ToList();

            if(categories is not null && categories.Count() > 0)
            {
                foreach (var category in categories)
                {
                    category.SubCategories = await _subCategoryService.GetSubCategoriesByCategory(tenantId, category.CategoryId);
                }
                return categories;
            }
            return new List<Category>();
        }

        public async Task<bool> Delete(Guid tenantId, Guid categoryId)
        {
            var context = LoadContext();
            Category category = _appDbContext.Categories
                .Where(x => x.TenantId == tenantId && x.CategoryId == categoryId)
                .First();
            category.Active = false;
            _appDbContext.Update(category);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
