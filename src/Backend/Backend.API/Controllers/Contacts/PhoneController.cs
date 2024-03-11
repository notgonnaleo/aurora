using Backend.Domain.Entities.Contacts;
using Backend.Infrastructure.Services.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Phones
{
    [ApiController]
    [Route("Phones")]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneService _phoneService;

        public PhoneController(PhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult GetPhones(Guid tenantId)
        {
            try
            {
                return Ok(_phoneService.GetPhones(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetPhoneById(Guid tenantId, Guid phoneId)
        {
            try
            {
                return Ok(_phoneService.GetPhoneById(tenantId, phoneId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddPhone(Phone phone)
        {
            try
            {
                return Ok(_phoneService.AddPhone(phone));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdatePhone(Phone phone)
        {
            try
            {
                return Ok(_phoneService.UpdatePhone(phone));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeletePhone(Guid tenantId, Guid phoneId)
        {
            try
            {
                return Ok(_phoneService.DeletePhone(tenantId, phoneId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

