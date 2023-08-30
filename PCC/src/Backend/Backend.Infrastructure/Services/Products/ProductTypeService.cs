using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductTypeService
    {
        private readonly AppDbContext _appDbContext;

        public ProductTypeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ProductType>> Get()
        {
            try
            {
                IEnumerable<ProductType> productType = _appDbContext.ProductTypes.ToList();
                return productType;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ProductType> GetById(Guid Id)
        {
            try
            {
                ProductType productType = _appDbContext.ProductTypes.Where(x => x.Id == Id).First();
                return productType;
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
                productType.Id = Guid.NewGuid();
                productType.Created = DateTime.Now;
                productType.CreatedBy = Guid.NewGuid(); // TODO: Get it from the user account id while it's log in.

                _appDbContext.ProductTypes.Add(productType);
                _appDbContext.SaveChanges();
                return productType;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> Update(ProductType productType)
        {
            try
            {
                productType = new ProductType() // Updating the header info from the product.
                {
                    Id = productType.Id,
                    Name = productType.Name,
                    Description = productType.Description,
                    Updated = null,
                    UpdatedBy = null // TODO: Get it from the user account id while it's log in.
                };
                _appDbContext.Update(productType);
                var response = _appDbContext.SaveChanges();

                if (response <= 0)
                    throw new Exception("Failed while trying to update the item.");

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                ProductType productType = _appDbContext.ProductTypes.Where(x => x.Id == Id).First();
                productType.Active = false;

                _appDbContext.Update(productType);
                var response = _appDbContext.SaveChanges();

                if (response <= 0)
                    throw new Exception("Failed while trying to delete the item.");

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
