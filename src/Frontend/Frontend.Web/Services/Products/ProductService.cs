using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

namespace Frontend.Web.Services.Products
{
    public class ProductService
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductService(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }
        public void Send(params object[] parameters)
        {
            // Do something with the parameters
            // For example, you can send them somewhere

            foreach (var param in parameters)
            {
                Console.WriteLine($"Parameter: {param}");
            }
        }

        /*
        public RouteBuilder<Product> Send(string tenantId)
        {
            return new RouteBuilder<Product>()
                .BuildRoute(Endpoints.Products,
                Methods.Default.GET,
                new RouteParameters().BuildParameterString(new RouteParameters()
                {
                    Key = 1,
                    ParameterName = "tenantId",
                    ParameterValue = tenantId,
                }),
                null);
        }
        */

        public async Task<List<Product>> GetProducts(string tenantId)
        {
            Send(tenantId); // maybe a dictionary? or a object again? idk this shit is confusing as fuck
            //return await _httpClientRepository.Get<Product>(routeBuilder);
            return new List<Product> { new Product() };
        }
    }
}
