using Backend.Domain.Entities.Profiles;
using Backend.Infrastructure.Services.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Profiles
{
    [ApiController]
    [Route("Profiles")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult GetProfiles(Guid tenantId)
        {
            try
            {
                return Ok(_profileService.GetProfiles(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetProfileById(Guid tenantId, Guid profileId)
        {
            try
            {
                return Ok(_profileService.GetProfileById(tenantId, profileId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddProfile(Profile profile)
        {
            try
            {
                return Ok(_profileService.AddProfile(profile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateProfile(Profile profile)
        {
            try
            {
                return Ok(_profileService.UpdateProfile(profile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteProfile(Guid tenantId, Guid profileId)
        {
            try
            {
                return Ok(_profileService.DeleteProfile(tenantId, profileId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

