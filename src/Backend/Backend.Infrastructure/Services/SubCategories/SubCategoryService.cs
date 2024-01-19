using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                return _appDbContext.SubCategories
                    .Where(x => x.TenantId == tenantId)
                    .ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SubCategory> GetById(Guid tenantId, Guid subcategoryId)
        {
            try
            {
                return _appDbContext.SubCategories
                     .FirstOrDefault(x => x.TenantId == tenantId && x.SubCategoryId == subcategoryId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SubCategory> Add(SubCategory subCategory)
        {
            try
            {
                var context = LoadContext();
                subCategory.SubCategoryId = Guid.NewGuid();
                subCategory.CreatedBy = context.UserId;
                subCategory.Updated = null;
                subCategory.UpdatedBy = null;
                subCategory.Active = true;
                
                _appDbContext.SubCategories.Add(subCategory);
                if(await _appDbContext.SaveChangesAsync() > 0)
                    return subCategory;

                throw new Exception("Failed while saving new item.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SubCategory> Update(SubCategory subCategory)
        {
            try
            {
                subCategory.SubCategoryName = subCategory.SubCategoryName;
                subCategory.Updated = DateTime.Now;
                subCategory.UpdatedBy = Guid.NewGuid();
                _appDbContext.Update(subCategory);
                if (!(await _appDbContext.SaveChangesAsync() > 0))
                    throw new Exception("Did not update.");
                return subCategory;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategory(Guid tenantId, Guid categoryId)
        {
            try
            {
                return await _appDbContext.SubCategories
                    .Where(x => x.TenantId == tenantId && x.CategoryId == categoryId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

