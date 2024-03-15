using Backend.Domain.Entities.Contacts;
using Backend.Infrastructure.Services.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.EmailAddresses
{
    namespace Backend.API.Controllers.Contacts
    {
        [ApiController]
        [Route("EmailAddresses")]
        public class EmailAddressController : ControllerBase
        {
            private readonly EmailAddressService _emailAddressService;

            public EmailAddressController(EmailAddressService emailAddressService)
            {
                _emailAddressService = emailAddressService;
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpGet]
            [Route("List")]
            public ActionResult GetEmails(Guid tenantId)
            {
                try
                {
                    return Ok(_emailAddressService.GetEmails(tenantId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpGet]
            [Route("Find")]
            public ActionResult GetEmailById(Guid tenantId, Guid emailAddressId)
            {
                try
                {
                    return Ok(_emailAddressService.GetEmailById(tenantId, emailAddressId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpPost]
            [Route("Add")]
            public ActionResult AddEmail(Email email)
            {
                try
                {
                    return Ok(_emailAddressService.AddEmail(email));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpPut]
            [Route("Update")]
            public ActionResult UpdateEmail(Email email)
            {
                try
                {
                    return Ok(_emailAddressService.UpdateEmail(email));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [TypeFilter(typeof(ValidateUserContextAttribute))]
            [HttpDelete]
            [Route("Delete")]
            public ActionResult DeleteEmail(Guid tenantId, Guid emailAddressId)
            {
                try
                {
                    return Ok(_emailAddressService.DeleteEmail(tenantId, emailAddressId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }

}
