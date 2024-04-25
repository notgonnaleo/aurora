﻿using Backend.Domain.Entities.Contacts;
using Backend.Domain.Entities.Profiles;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

namespace Frontend.Web.Repository.Contacts
{
    namespace Frontend.Web.Services.Profiles
    {
        public class ProfileRepository
        {
            private readonly HttpClientRepository _httpClientRepository;

            public ProfileRepository(HttpClientRepository httpClientRepository)
            {
                _httpClientRepository = httpClientRepository;
            }

            public async Task<ApiResponse<IEnumerable<Profile>>> GetProfiles(string tenantId)
            {
                var parameters = new RouteParameterRequest() { ParameterName = Methods.Profiles.GET.GetProfiles.tenantId, ParameterValue = tenantId };
                var request = new RouteBuilder<Profile>().Send(Endpoints.Profiles, Methods.Default.GET, parameters);
                return await _httpClientRepository.Get(request);
            }

            public async Task<ApiResponse<Profile>> GetProfile(string tenantId, string profileId)
            {
                var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Profiles.GET.GetProfile.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Profiles.GET.GetProfile.profileId,
                    ParameterValue = profileId,
                }
            };
                var request = new RouteBuilder<Profile>().SendMultiple(Endpoints.Profiles, Methods.Default.FIND, parameters);
                return await _httpClientRepository.Find(request);
            }

            public async Task<ApiResponse<Profile>> CreateProfile(Profile profile)
            {
                var model = new RouteBuilder<Profile>().Send(Endpoints.Profiles, Methods.Default.POST, profile);
                return await _httpClientRepository.Post<Profile, Profile>(model);
            }

            public async Task<ApiResponse<bool>> UpdateProfile(Profile profile)
            {
                var model = new RouteBuilder<Profile>().Send(Endpoints.Profiles, Methods.Default.PUT, profile);
                var response = await _httpClientRepository.Put(model);
                return new ApiResponse<bool>()
                {
                    Success = response.Success,
                    ResultBoolean = response.ResultBoolean,
                    ErrorMessage = response.ErrorMessage,
                    StatusCode = response.StatusCode
                };
            }

            public async Task<bool> DeleteProfile(string tenantId, string profileId)
            {
                var parameters = new List<RouteParameterRequest>()
                {
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Profiles.DELETE.DeleteProfile.tenantId,
                        ParameterValue = tenantId,
                    },
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Profiles.DELETE.DeleteProfile.profileId,
                        ParameterValue = profileId,
                    }
                };
                var request = new RouteBuilder<Profile>().SendMultiple(Endpoints.Profiles, Methods.Default.DELETE, parameters);
                return (await _httpClientRepository.Put(request)).Success;
            }
        }
    }

}
