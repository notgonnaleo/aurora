using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Infrastructure.Enums.Modules.ModulesEnum;
using Backend.Infrastructure.Context;
using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Enums.Localization;
using Backend.Domain.Entities.Products;
using static Backend.Infrastructure.Enums.Modules.Methods;


namespace Backend.Infrastructure.Services.Agents
{
    public class AgentService : Service
    {

        private readonly AppDbContext _appDbContext;

        public AgentService(AppDbContext appDbContext, UserContextService main) : base(main)
        {
            _appDbContext = appDbContext;
        }

        public Agent Add(Agent agent)
        {
            var context = LoadContext();
            agent.TenantId = context.Tenant.Id;
            agent.AgentId = Guid.NewGuid();
            agent.Name = agent.Name;
            agent.AgentTypeId = agent.AgentTypeId;
            agent.UserId = context.UserId;
            agent.Created = DateTime.UtcNow;
            agent.CreatedBy = context.UserId;
            agent.Updated = DateTime.UtcNow;
            agent.UpdatedBy = context.UserId;
            _appDbContext.Agents.Add(agent);
            _appDbContext.SaveChanges();
            return agent;
        }

        public IEnumerable<Agent> Get()
        {
            var context = LoadContext();
            return _appDbContext.Agents.Where(x => x.TenantId == context.Tenant.Id && x.Active).ToList();
        }

        public Agent? GetById(Guid tenantId, Guid agentId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            return _appDbContext.Agents.FirstOrDefault(x => x.AgentId == agentId && x.TenantId == context.Tenant.Id);
        }

        public bool Update(Agent model)
        {
            var context = LoadContext();
            ValidateTenant(model.TenantId);
            model.Updated = DateTime.Now;
            model.UpdatedBy = context.UserId;
            model.Active = true;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid Id)
        {
            var context = LoadContext();
            Agent agents = _appDbContext.Agents.Where(x => x.AgentId == Id && x.TenantId == context.Tenant.Id).First();
            agents.Active = false;

            _appDbContext.Update(agents);
            var response = _appDbContext.SaveChanges();

            if (response <= 0)
                throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            return true;
        }
    }
}
