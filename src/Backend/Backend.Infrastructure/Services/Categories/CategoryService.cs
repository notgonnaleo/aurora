using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Category;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Categorys
{
    public class CategoryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContextService _userContextService;

        public CategoryService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserContextService userContextService)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<Category>> Get(Guid tenantId)
        {
            try
            {
                return _appDbContext.Categorys
                    .Where(x => x.TenantId == tenantId)
                    .ToList();



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Category> GetById(Guid categoryId, Guid tenantId)
        {
            try
            {
                return _appDbContext.Categorys
                     .FirstOrDefault(x => x.TenantId == tenantId && x.CategoryId == categoryId);
                     

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Category> Add(Category category)
        {
            try
            {
                var context = _userContextService.LoadContext();
                category.CategoryId = Guid.NewGuid();
                category.CreatedBy = context.UserId;
                category.Created = DateTime.Now;
                category.Updated = null;
                category.UpdatedBy = null;
                _appDbContext.Categorys.Add(category);
                _appDbContext.SaveChanges();

                return category;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> Update(Category category, Guid categoryId)
        {
            var Category = await _appDbContext.Categorys.FindAsync(categoryId);

            if (Category == null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }

            Category.CategoryName = category.CategoryName;
            Category.Updated = DateTime.Now;
            Category.UpdatedBy = Guid.NewGuid();

            await _appDbContext.SaveChangesAsync();

            return Category;
        }
    }
}
