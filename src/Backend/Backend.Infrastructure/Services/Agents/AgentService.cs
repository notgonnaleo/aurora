using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Infrastructure.Enums.Modules.Methods;
using Backend.Infrastructure.Context;
using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Authentication.Tenants;


namespace Backend.Infrastructure.Services.Agents
{
    public class AgentService
    {

        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContextService _userContextService;

        public AgentService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserContextService userContextService)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        public async Task<Agent> Add(Agent agent)
        {
            try
            {
                var context = _userContextService.LoadContext();
                agent.UserId = context.UserId;
                agent.TenantId = context.Tenant.Id;
                agent.Id = Guid.NewGuid();
                agent.UserId = context.UserId;
                agent.Created = DateTime.UtcNow;
                agent.CreatedBy = context.UserId;
                agent.Updated = DateTime.UtcNow;
                agent.UpdatedBy = context.UserId;
                _appDbContext.Agents.Add(agent);
                _appDbContext.SaveChanges();
                return agent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Agent>> Get(Guid tenantId)
        {
            try
            {
                return _appDbContext.Agents
                    .Where(x => x.TenantId == tenantId && x.Active == true)
                    .ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Agent> GetById(Guid id, Guid tenantId)
        {
            try
            {
                return  _appDbContext.Agents
                        .Where(x => x.Id == id && x.TenantId == tenantId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Agent> Update(Guid Id,Agent agents,Guid tenantId)
        {
            try
            {
                
                var Agents = _appDbContext.Agents
                        .Where(x => x.Id == Id && x.TenantId == tenantId).FirstOrDefault();

                if (Agents == null)
                {
                    throw new ArgumentException("Não encontrado.");
                }

                Agents.Name = agents.Name;
                Agents.Updated = DateTime.Now;
                Agents.UpdatedBy = Guid.NewGuid();  
                Agents.Active= true;

                await _appDbContext.SaveChangesAsync();

                return Agents;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(Guid Id, Guid tenantId)
        {
            try
            {
                Agent agents = _appDbContext.Agents.Where(x => x.Id == Id && x.TenantId == tenantId).First();
                agents.Active = false;

                _appDbContext.Update(agents);
                var response = _appDbContext.SaveChanges();

                if (response <= 0)
                    throw new Exception("Failed while trying to delete the item.");

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
