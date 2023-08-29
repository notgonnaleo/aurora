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
    }
}
