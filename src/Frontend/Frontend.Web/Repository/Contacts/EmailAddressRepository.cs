using Backend.Domain.Entities.Contacts;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;
using static Backend.Infrastructure.Enums.Modules.Methods;

namespace Frontend.Web.Repository.Contacts
{
    public class EmailAddressRepository
    {
        private readonly HttpClientRepository _httpClientRepository;

        public EmailAddressRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<IEnumerable<Email>> GetEmails(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = EmailAddresses.GET.GetEmails.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Email>().Send(Endpoints.EmailAddresses, Methods.Default.GET, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }

        public async Task<Email> GetEmail(string tenantId, string emailAddressId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = EmailAddresses.GET.GetEmail.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = EmailAddresses.GET.GetEmail.emailAddressId,
                    ParameterValue = emailAddressId,
                }
            };
            var request = new RouteBuilder<Email>().SendMultiple(Endpoints.EmailAddresses, Methods.Default.FIND, parameters);
            return (await _httpClientRepository.Find(request)).Result;
        }

        public async Task<ApiResponse<Email>> CreateEmail(Email email)
        {
            var model = new RouteBuilder<Email>().Send(Endpoints.EmailAddresses, Methods.Default.POST, email);
            return await _httpClientRepository.Post(model);

        }

        public async Task<bool> UpdateEmail(Email email)
        {
            var model = new RouteBuilder<Email>().Send(Endpoints.EmailAddresses, Methods.Default.PUT, email);
            return (await _httpClientRepository.Put(model)).Success;
        }

        public async Task<bool> DeleteEmail(string tenantId, string emailAddressId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = EmailAddresses.DELETE.DeleteEmail.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = EmailAddresses.DELETE.DeleteEmail.emailAddressId,
                    ParameterValue = emailAddressId,
                }
            };
            var request = new RouteBuilder<Email>().SendMultiple(Endpoints.EmailAddresses, Methods.Default.DELETE, parameters);
            return (await _httpClientRepository.Put(request)).Success;
        }
    }
}
