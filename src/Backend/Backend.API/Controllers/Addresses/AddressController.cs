using Backend.Domain.Entities.Addresses;
using Backend.Infrastructure.Services.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Addresses
{
    namespace Backend.API.Controllers.Addresses
    {
        [ApiController]
        [Route("Addresses")]
        public class AddressController : ControllerBase
        {
            private readonly AddressService _addressService;

            public AddressController(AddressService addressService)
            {
                _addressService = addressService;
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpGet]
            [Route("List")]
            public ActionResult GetAddresses(Guid tenantId)
            {
                try
                {
                    return Ok(_addressService.GetAddresses(tenantId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpGet]
            [Route("Find")]
            public ActionResult GetAddressById(Guid tenantId, Guid addressId)
            {
                try
                {
                    return Ok(_addressService.GetAddressById(tenantId, addressId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpPost]
            [Route("Add")]
            public ActionResult AddAddress(Address address)
            {
                try
                {
                    return Ok(_addressService.AddAddress(address));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpPut]
            [Route("Update")]
            public ActionResult UpdateAddress(Address address)
            {
                try
                {
                    return Ok(_addressService.UpdateAddress(address));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpDelete]
            [Route("Delete")]
            public ActionResult DeleteAddress(Guid tenantId, Guid addressId)
            {
                try
                {
                    return Ok(_addressService.DeleteAddress(tenantId, addressId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }

}
