﻿using Backend.Domain.Entities.Category;
using Backend.Domain.Entities.SubCategory;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.SubCategories
{
    public class SubCategorieservice
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContextService _userContextService;

        public SubCategorieservice(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserContextService userContextService)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<SubCategory>> Get(Guid tenantId)
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

        public async Task<SubCategory> GetById(Guid subcategoryId,Guid tenantId)
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

        public async Task<SubCategory> Add(SubCategory subCategory, Guid categoryId)
        {
            try
            {
                // Verificar se o CategoryId existe antes de adicionar              

                if (categoryId != null)
                {
                    // Adicionar a SubCategory

                    var context = _userContextService.LoadContext();
                    subCategory.CategoryId = categoryId;
                    subCategory.SubCategoryId = Guid.NewGuid();
                    subCategory.CreatedBy = DateTime.Now;
                    subCategory.Updated = null;
                    subCategory.UpdatedBy = null;

                    _appDbContext.SubCategories.Add(subCategory);
                    await _appDbContext.SaveChangesAsync();

                    return subCategory;
                }
                else
                {
                    throw new ArgumentException("The Subcategory searched does not exist or is invalid");
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SubCategory> Update(SubCategory subCategory, Guid SubCategoryId)
        {
            var SubCategory = await _appDbContext.SubCategories.FindAsync(SubCategoryId);

            if (SubCategory == null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }

            SubCategory.SubCategoryName = subCategory.SubCategoryName;
            SubCategory.Updated = DateTime.Now;
            SubCategory.UpdatedBy = Guid.NewGuid();

            await _appDbContext.SaveChangesAsync();

            return SubCategory;
        }
    }
}

