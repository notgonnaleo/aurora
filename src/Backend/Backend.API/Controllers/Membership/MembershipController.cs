using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Memberships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.API.Controllers.Membership
{
    [ApiController]
    [Route("[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly MembershipService _membershipService;
        public MembershipController(MembershipService membershipService)
        {
            _membershipService = membershipService;
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetTenantsByUserId")]
        public ActionResult<IEnumerable<Tenant>> GetTenantsByUserId(Guid userId)
        {
            return Ok(_membershipService.GetTenantsByUserId(userId));
        }
    }
}
