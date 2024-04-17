using Backend.Domain.Entities.Addresses;
using Backend.Domain.Entities.Contacts;
using Backend.Domain.Entities.Profiles;
using Frontend.Web.Models.Client;
using Frontend.Web.Repository.Contacts;
using Frontend.Web.Repository.Contacts.Frontend.Web.Services.Addresses;
using Frontend.Web.Repository.Contacts.Frontend.Web.Services.Phones;
using Frontend.Web.Repository.Contacts.Frontend.Web.Services.Profiles;

namespace Frontend.Web.Services.Contacts
{
    public class ContactService
    {
        private readonly AddressRepository _addressRepository;
        private readonly EmailAddressRepository _emailAddressRepository;
        private readonly PhoneRepository _phoneRepository;
        private readonly ProfileRepository _profileRepository;

        public ContactService(
            AddressRepository addressRepository,
            EmailAddressRepository emailAddressRepository,
            PhoneRepository phoneRepository,
            ProfileRepository profileRepository)
        {
            _addressRepository = addressRepository;
            _emailAddressRepository = emailAddressRepository;
            _phoneRepository = phoneRepository;
            _profileRepository = profileRepository;
        }

        // Address methods
        public async Task<IEnumerable<Address>> GetAddresses(string tenantId)
        {
            return await _addressRepository.GetAddresses(tenantId);
        }

        public async Task<Address> GetAddress(string tenantId, string addressId)
        {
            return await _addressRepository.GetAddress(tenantId, addressId);
        }

        public async Task<ApiResponse<Address>> CreateAddress(Address address)
        {
            return await _addressRepository.CreateAddress(address);
        }

        public async Task<ApiResponse<bool>> UpdateAddress(Address address)
        {
            var response = await _addressRepository.UpdateAddress(address);
            return new ApiResponse<bool>()
            {
                Success = response.Success,
                ResultBoolean = response.ResultBoolean,
                ErrorMessage = response.ErrorMessage,
                StatusCode = response.StatusCode
            };
        }

        public async Task<bool> DeleteAddress(string tenantId, string addressId)
        {
            return await _addressRepository.DeleteAddress(tenantId, addressId);
        }

        // Email Address methods
        public async Task<IEnumerable<Email>> GetEmailAddresses(string tenantId)
        {
            return await _emailAddressRepository.GetEmails(tenantId);
        }

        public async Task<Email> GetEmailAddress(string tenantId, string emailAddressId)
        {
            return await _emailAddressRepository.GetEmail(tenantId, emailAddressId);
        }

        public async Task<ApiResponse<Email>> CreateEmailAddress(Email email)
        {
            return await _emailAddressRepository.CreateEmail(email);
        }

        public async Task<ApiResponse<bool>> UpdateEmailAddress(Email email)
        {
            var response = await _emailAddressRepository.UpdateEmail(email);
            return new ApiResponse<bool>()
            {
                Success = response.Success,
                ResultBoolean = response.ResultBoolean,
                ErrorMessage = response.ErrorMessage,
                StatusCode = response.StatusCode
            };
        }

        public async Task<bool> DeleteEmailAddress(string tenantId, string emailAddressId)
        {
            return await _emailAddressRepository.DeleteEmail(tenantId, emailAddressId);
        }

        // Phone methods
        public async Task<IEnumerable<Phone>> GetPhones(string tenantId)
        {
            return await _phoneRepository.GetPhones(tenantId);
        }

        public async Task<Phone> GetPhone(string tenantId, string phoneId)
        {
            return await _phoneRepository.GetPhone(tenantId, phoneId);
        }

        public async Task<Phone> CreatePhone(Phone phone)
        {
            return (await _phoneRepository.CreatePhone(phone)).Result;
        }

        public async Task<bool> UpdatePhone(Phone phone)
        {
            return await _phoneRepository.UpdatePhone(phone);
        }

        public async Task<bool> DeletePhone(string tenantId, string phoneId)
        {
            return await _phoneRepository.DeletePhone(tenantId, phoneId);
        }

        // Profile methods
        public async Task<IEnumerable<Profile>> GetProfiles(string tenantId)
        {
            return await _profileRepository.GetProfiles(tenantId);
        }

        public async Task<Profile> GetProfile(string tenantId, string profileId)
        {
            return await _profileRepository.GetProfile(tenantId, profileId);
        }

        public async Task<Profile> CreateProfile(Profile profile)
        {
            return (await _profileRepository.CreateProfile(profile)).Result;
        }

        public async Task<bool> UpdateProfile(Profile profile)
        {
            return await _profileRepository.UpdateProfile(profile);
        }

        public async Task<bool> DeleteProfile(string tenantId, string profileId)
        {
            return await _profileRepository.DeleteProfile(tenantId, profileId);
        }
    }
}
