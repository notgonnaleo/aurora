using Backend.Domain.Entities.Agents.Response;
using Backend.Domain.Entities.Notifications;
using Backend.Domain.Entities.Orders;
using Backend.Domain.Entities.Products;
using Backend.Domain.Enums.Agents;
using Backend.Domain.Enums.Orders;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Modules;
using Backend.Infrastructure.Migrations.AppDbMigration;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using static Backend.Infrastructure.Enums.Modules.Methods;

namespace Backend.API.Controllers.Audit
{
    [ApiController]
    [Route("AuditLog")]
    public class AuditLogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly AuthDbContext _authDbContext;
        public AuditLogController(AppDbContext appDbContext, AuthDbContext authDbContext)
        {
            _appDbContext = appDbContext;
            _authDbContext = authDbContext;
        }

        //[TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetNotifications")]
        public ActionResult GetNotifications(Guid tenantId)
        {
            var today = DateTime.Today;  // Adjusted to DateTime.Today for consistency
            var notifications = new List<Notification>();

            // Auth db queries
            var membership = _authDbContext.Memberships.Where(x => x.TenantId == tenantId).ToList();
            var tenantUsers = _authDbContext.Users.Where(x => membership.Select(m => m.UserId).Contains(x.Id)).ToList();
            var tenantUserDict = tenantUsers.ToDictionary(user => user.Id, user => user.Username);

            var allAgents = _appDbContext.Agents.Where(x => x.TenantId == tenantId && x.Active).ToList();
            // Combine the queries to minimize the number of database calls
            var agents = allAgents
                .Where(x => x.Created.HasValue && (x.Created.Value.Date >= today || x.Created.Value.Date <= today) || x.Updated.HasValue && x.Updated.Value.Date == today)
                .Select(agent => new
                {
                    agent.AgentId,
                    agent.Name,
                    agent.AgentTypeId,
                    agent.CreatedBy,
                    Timestamp = agent.Updated == null ? agent.Updated : agent.Created,
                    ActorName = tenantUserDict.ContainsKey(agent.CreatedBy.Value) ? tenantUserDict[agent.CreatedBy.Value] : "Unknown"
                })
            .ToList();

            var orders = _appDbContext.Orders
                .Where(x => x.TenantId == tenantId && x.Active && (x.Created.HasValue && (x.Created.Value.Date >= today || x.Created.Value.Date <= today) || x.Updated.HasValue && x.Updated.Value.Date == today))
                .Include(x => x.Seller).Where(x => allAgents.Select(x => x.AgentId).Contains(x.SellerId))
                .Include(x => x.Customer).Where(x => allAgents.Select(x => x.AgentId).Contains(x.CustomerId))
                .Select(order => new
                {
                    order.OrderId,
                    order.OrderCode,
                    order.Seller,
                    order.Customer,
                    order.CreatedBy,
                    Timestamp = order.Updated == null ? order.Updated : order.Created,
                    ActorName = tenantUserDict.ContainsKey(order.CreatedBy.Value) ? tenantUserDict[order.CreatedBy.Value] : "Unknown"
                })
                .ToList();

            var products = _appDbContext.Products
                .Where(x => x.TenantId == tenantId && x.Active && (x.Created.HasValue && (x.Created.Value.Date >= today || x.Created.Value.Date <= today) || x.Updated.HasValue && x.Updated.Value.Date == today))
                .Select(product => new
                {
                    product.ProductId,
                    product.Description,
                    product.SKU,
                    product.Name,
                    product.ProductTypeId,
                    product.CreatedBy,
                    Timestamp = product.Updated == null ? product.Updated : product.Created,
                    ActorName = tenantUserDict.ContainsKey(product.CreatedBy.Value) ? tenantUserDict[product.CreatedBy.Value] : "Unknown"
                })
                .ToList();

            // Aggregate notifications
            notifications.AddRange(agents.Select(agent => new Notification
            {
                NotificationId = agent.AgentId,
                NotificationName = agent.Name,
                NotificationItemTypeId = agent.AgentTypeId,
                NotificationTypeId = (int)ModulesEnum.Agents,
                NotificationActor = new AgentThumbnailResponse
                {
                    AgentId = agent.CreatedBy,
                    AgentDisplayName = agent.ActorName
                },
                NotificationTimestamp = agent.Timestamp ?? DateTime.Now // Fallback to current date-time
            }));

            notifications.AddRange(orders.Select(order => new Notification
            {
                NotificationId = order.OrderId,
                NotificationTypeId = (int)ModulesEnum.Orders,
                NotificationItemCode = order.OrderCode,
                NotificationEmployeeName = order.Seller.Name,
                NotificationCustomerName = order.Customer.Name,
                NotificationActor = new AgentThumbnailResponse
                {
                    AgentId = order.CreatedBy,
                    AgentDisplayName = order.ActorName
                },
                NotificationTimestamp = order.Timestamp ?? DateTime.Now // Fallback to current date-time
            }));

            notifications.AddRange(products.Select(product => new Notification
            {
                NotificationId = product.ProductId,
                NotificationTypeId = (int)ModulesEnum.Products,
                NotificationItemCode = product.SKU,
                NotificationDescription = product.Description,
                NotificationItemTypeId = product.ProductTypeId,
                NotificationActor = new AgentThumbnailResponse
                {
                    AgentId = product.CreatedBy,
                    AgentDisplayName = product.ActorName
                },
                NotificationTimestamp = product.Timestamp ?? DateTime.Now // Fallback to current date-time
            }));

            return Ok(notifications);
        }
    }
}
