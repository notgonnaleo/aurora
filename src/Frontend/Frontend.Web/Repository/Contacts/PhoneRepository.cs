using Backend.Domain.Entities.Contacts;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

namespace Frontend.Web.Repository.Contacts
{
    namespace Frontend.Web.Services.Phones
    {
        public class PhoneRepository
        {
            private readonly HttpClientRepository _httpClientRepository;

            public PhoneRepository(HttpClientRepository httpClientRepository)
            {
                _httpClientRepository = httpClientRepository;
            }

            public async Task<IEnumerable<Phone>> GetPhones(string tenantId)
            {
                var parameters = new RouteParameterRequest() { ParameterName = Methods.Phones.GET.GetPhones.tenantId, ParameterValue = tenantId };
                var request = new RouteBuilder<Phone>().Send(Endpoints.Phones, Methods.Default.GET, parameters);
                return await _httpClientRepository.Get(request);
            }

            public async Task<Phone> GetPhone(string tenantId, string phoneId)
            {
                var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Phones.GET.GetPhone.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Phones.GET.GetPhone.phoneId,
                    ParameterValue = phoneId,
                }
            };
                var request = new RouteBuilder<Phone>().SendMultiple(Endpoints.Phones, Methods.Default.FIND, parameters);
                return await _httpClientRepository.GetById(request);
            }

            public async Task<Phone> CreatePhone(Phone phone)
            {
                var model = new RouteBuilder<Phone>().Send(Endpoints.Phones, Methods.Default.POST, phone);
                var response = await _httpClientRepository.Post(model);
                return await response.Content.ReadFromJsonAsync<Phone>();
            }

            public async Task<bool> UpdatePhone(Phone phone)
            {
                var model = new RouteBuilder<Phone>().Send(Endpoints.Phones, Methods.Default.PUT, phone);
                return await _httpClientRepository.Put(model);
            }

            public async Task<bool> DeletePhone(string tenantId, string phoneId)
            {
                var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Phones.DELETE.DeletePhone.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Phones.DELETE.DeletePhone.phoneId,
                    ParameterValue = phoneId,
                }
            };
                var request = new RouteBuilder<Phone>().SendMultiple(Endpoints.Phones, Methods.Default.DELETE, parameters);
                return await _httpClientRepository.Put(request);
            }
        }
    }

}
