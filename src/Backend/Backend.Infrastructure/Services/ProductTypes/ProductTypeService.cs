using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.ProductTypes;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.ProductTypes
{
    public class ProductTypeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContextService _userContextService;

        public ProductTypeService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserContextService userContextService)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ProductType>> Get()
        {
            try
            {
                return _appDbContext.ProductTypes
                    .Where(x =>  x.Active == true)
                    .ToList();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ProductType> GetById(int Id)
        {
            try
            {
                return _appDbContext.ProductTypes.Where(x => x.Id == Id).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ProductType> Add(ProductType productType)
        {
            try
            {
                _appDbContext.ProductTypes.Add(productType);
                _appDbContext.SaveChanges();

                return productType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductType> Update(ProductType productType, int id)
        {
            ProductType ProductTypeId = await GetById(id);

            if (productType == null)
            {
                throw new Exception($"Não encontrado {id}");
            }
            ProductTypeId.Name = productType.Name;
            ProductTypeId.Description = productType.Description;
            ProductTypeId.Active = productType.Active;

            _appDbContext.ProductTypes.Update(ProductTypeId);
            _appDbContext.SaveChanges();

            return ProductTypeId;
        }
    }

}
