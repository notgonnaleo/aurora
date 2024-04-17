using Backend.Domain.Entities.Addresses;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Backend.Infrastructure.Enums.Modules;
using System.Net.Http.Json;
using Frontend.Web.Models.Client;

namespace Frontend.Web.Repository.Contacts
{
    namespace Frontend.Web.Services.Addresses
    {
        public class AddressRepository
        {
            private readonly HttpClientRepository _httpClientRepository;

            public AddressRepository(HttpClientRepository httpClientRepository)
            {
                _httpClientRepository = httpClientRepository;
            }

            public async Task<IEnumerable<Address>> GetAddresses(string tenantId)
            {
                var parameters = new RouteParameterRequest() { ParameterName = Methods.Addresses.GET.GetAddresses.tenantId, ParameterValue = tenantId };
                var request = new RouteBuilder<Address>().Send(Endpoints.Addresses, Methods.Default.GET, parameters);
                return (await _httpClientRepository.Get(request)).Result;
            }

            public async Task<Address> GetAddress(string tenantId, string addressId)
            {
                var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Addresses.GET.GetAddress.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Addresses.GET.GetAddress.addressId,
                    ParameterValue = addressId,
                }
            };
                var request = new RouteBuilder<Address>().SendMultiple(Endpoints.Addresses, Methods.Default.FIND, parameters);
                return (await _httpClientRepository.Find(request)).Result;
            }

            public async Task<ApiResponse<Address>> CreateAddress(Address address)
            {
                var model = new RouteBuilder<Address>().Send(Endpoints.Addresses, Methods.Default.POST, address);
                return await _httpClientRepository.Post(model);
            }

            public async Task<bool> UpdateAddress(Address address)
            {
                var model = new RouteBuilder<Address>().Send(Endpoints.Addresses, Methods.Default.PUT, address);
                return (await _httpClientRepository.Put(model)).Success;
            }

            public async Task<bool> DeleteAddress(string tenantId, string addressId)
            {
                var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Addresses.DELETE.DeleteAddress.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Addresses.DELETE.DeleteAddress.addressId,
                    ParameterValue = addressId,
                }
            };
                var request = new RouteBuilder<Address>().SendMultiple(Endpoints.Addresses, Methods.Default.DELETE, parameters);
                return (await _httpClientRepository.Put(request)).Success;
            }
        }
    }

}
