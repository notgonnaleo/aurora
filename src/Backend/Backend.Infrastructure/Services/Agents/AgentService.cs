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


namespace Backend.Infrastructure.Services.Agents
{
    public class AgentService : Service
    {

        private readonly AppDbContext _appDbContext;

        public AgentService(AppDbContext appDbContext, UserContextService main) : base(main)
        {
            _appDbContext = appDbContext;
        }

        public Domain.Entities.Agents.Agent Add(Domain.Entities.Agents.Agent agent)
        {
            var context = LoadContext();
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

        public IEnumerable<Domain.Entities.Agents.Agent> Get()
        {
            var context = LoadContext();
            return _appDbContext.Agents.Where(x => x.TenantId == context.Tenant.Id && x.Active == true).ToList();
        }

        public Domain.Entities.Agents.Agent? GetById(Guid tenantId, Guid agentId)
        {
            var context = LoadContext();
            return _appDbContext.Agents.FirstOrDefault(x => x.Id == agentId && x.TenantId == context.Tenant.Id);
        }

        public bool Update(Domain.Entities.Agents.Agent model)
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
            Agent agents = _appDbContext.Agents.Where(x => x.Id == Id && x.TenantId == context.Tenant.Id).First();
            agents.Active = false;

            _appDbContext.Update(agents);
            var response = _appDbContext.SaveChanges();

            if (response <= 0)
                throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            return true;
        }
    }
}
