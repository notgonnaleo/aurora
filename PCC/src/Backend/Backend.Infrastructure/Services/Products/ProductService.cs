using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                IEnumerable<Product> products = _appDbContext.Products.ToList();
                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Product> GetById(Guid Id)
        {
            try
            {
                Product product = _appDbContext.Products.Where(x => x.Id == Id).First();
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Product> Add(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid();
                product.Created = DateTime.Now;
                product.CreatedBy = Guid.NewGuid(); // TODO: Get it from the user account id while it's log in.

                _appDbContext.Products.Add(product);
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                product = new Product() // Updating the header info from the product.
                {
                    Id = product.Id,
                    Name = product.Name,
                    SKU = product.SKU,
                    Description = product.Description,
                    Updated = DateTime.Now,
                    UpdatedBy = Guid.NewGuid() // TODO: Get it from the user account id while it's log in.
                };
                _appDbContext.Update(product);
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
                Product product = _appDbContext.Products.Where(x => x.Id == Id).First();
                product.Active = false;

                _appDbContext.Update(product);
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
