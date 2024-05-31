using Backend.Domain.Enums.Agents;
using Backend.Domain.Enums.Orders;
using Backend.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Backend.API.Controllers.Audit
{
    [ApiController]
    [Route("AuditLog")]
    public class AuditLogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public AuditLogController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetNotifications")]
        public ActionResult GetNotifications(Guid tenantId)
        {
            var today = DateTime.Now;

            var agents = _appDbContext.Agents
                .Where(x => x.TenantId == tenantId && x.Created == today || x.Updated == today && x.Active)
                .ToList();

            var orders = _appDbContext.Orders
                .Where(x => x.TenantId == tenantId && x.Created == today || x.Updated == today && x.Active)
                .ToList();

            var products = _appDbContext.Products
                .Where(x => x.TenantId == tenantId && x.Created == today || x.Updated == today && x.Active)
                .ToList();

            var inventory = _appDbContext.Stocks
                .Where(x => x.TenantId == tenantId && x.Created == today || x.Updated == today && x.Active)
                .ToList();

            return Ok();
        }
    }
}
