using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Context;
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

        public IEnumerable<SubCategory> Get(Guid tenantId)
        {
            return _appDbContext.SubCategories
                .Where(x => x.TenantId == tenantId);
        }

        public async Task<SubCategory> GetById(Guid tenantId, Guid subcategoryId)
        {
            return _appDbContext.SubCategories
                .FirstOrDefault(x => x.TenantId == tenantId && x.SubCategoryId == subcategoryId);

        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategory(Guid tenantId, Guid categoryId)
        {
            return _appDbContext.SubCategories
                .Where(x => x.TenantId == tenantId && x.CategoryId == categoryId && x.Active);
        }

        public async Task<SubCategory> Add(SubCategory subCategory)
        {
            var context = LoadContext();
            subCategory = subCategory.Create(subCategory, context.UserId);
            subCategory.ValidateFields();

            _appDbContext.SubCategories.Add(subCategory);
            if(await _appDbContext.SaveChangesAsync() > 0)
                return subCategory;

            throw new Exception("Failed while saving new item.");
        }

        public async Task<bool> Update(SubCategory subCategory)
        {
            subCategory.SubCategoryName = subCategory.SubCategoryName;
            subCategory.Updated = DateTime.Now;
            subCategory.UpdatedBy = Guid.NewGuid();
            subCategory.ValidateFields();

            _appDbContext.Update(subCategory);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid tenantId, Guid categoryId, Guid subcategoryId)
        {
            SubCategory subCategory = _appDbContext.SubCategories
                .Where(x => x.TenantId == tenantId && x.CategoryId == categoryId && x.SubCategoryId == subcategoryId)
                .First();
            subCategory.Active = false;
            _appDbContext.Update(subCategory);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}

