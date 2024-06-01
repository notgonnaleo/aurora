using Backend.Domain.Enums.Agents;
using Backend.Domain.Enums.Orders;
using Backend.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Backend.API.Controllers.Kpis
{
    [ApiController]
    [Route("Kpi")]
    public class KpiController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public KpiController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetDashboardAnalytics")]
        public ActionResult GetDashboardKpis(Guid tenantId)
        {
            try
            {
                return Ok(new
                {
                    TotalProducts = _appDbContext.Products.Where(x => x.TenantId == tenantId && x.Active).Count(),
                    TotalCustomers = _appDbContext.Agents.Where(x => x.TenantId == tenantId && x.AgentTypeId == (int)AgentTypes.Customer).Count(),
                    TotalOpenOrders = _appDbContext.Orders.Where(x => x.TenantId == tenantId && x.OrderStatusId == (int)OrdersStatusEnums.Open && x.Active).Count(),
                    TotalClosedOrders = _appDbContext.Orders.Where(x => x.TenantId == tenantId && x.OrderStatusId == (int)OrdersStatusEnums.Done && x.Active).Count(),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetTransactionsAnalytics")]
        public ActionResult GetTransactionsAnalytics(Guid tenantId)
        {
            var agents = _appDbContext.Agents.Where(x => x.TenantId == tenantId && x.Active).ToList();

            var transactions = _appDbContext.Orders
                .Where(x => x.TenantId == tenantId && x.Active)
                .OrderByDescending(y => y.Created)
                .Take(6)
                .Include(o => o.Customer)
                .Include(o => o.Seller)
                .ToList();

            var filteredTransactions = transactions
                .Where(t => agents.Any(a => a.AgentId == t.CustomerId) && agents.Any(a => a.AgentId == t.SellerId))
                .ToList();

            return Ok(filteredTransactions);
        }
    }
}
