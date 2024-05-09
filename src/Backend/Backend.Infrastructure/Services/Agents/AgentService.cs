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
using Backend.Domain.Entities.Agents.Response;
using Backend.Domain.Enums.Agents;

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
            agent.AgentId = Guid.NewGuid();
            agent.Name = agent.Name;
            agent.AgentTypeId = agent.AgentTypeId;
            agent.UserId = context.UserId;
            agent.Created = DateTime.UtcNow;
            agent.CreatedBy = context.UserId;
            agent.Updated = DateTime.UtcNow;
            agent.UpdatedBy = context.UserId;
            agent.Active = true;
            _appDbContext.Agents.Add(agent);
            _appDbContext.SaveChanges();
            return agent;
        }

        public IEnumerable<Domain.Entities.Agents.Agent> Get()
        {
            var context = LoadContext();
            return _appDbContext.Agents.Where(x => x.TenantId == context.Tenant.Id && x.Active).ToList();
        }

        public Domain.Entities.Agents.Agent? GetById(Guid tenantId, Guid agentId)
        {
            if(tenantId == Guid.Empty || agentId == Guid.Empty)
                return null;

            var context = LoadContext();
            ValidateTenant(tenantId);
            return _appDbContext.Agents.FirstOrDefault(x => x.AgentId == agentId && x.TenantId == context.Tenant.Id);
        }

        public Domain.Entities.Agents.Agent? GetEmployee(Guid tenantId, Guid agentId)
        {
            if (tenantId == Guid.Empty || agentId == Guid.Empty)
                return null;

            var context = LoadContext();
            ValidateTenant(tenantId);
            var seller = _appDbContext.Agents
                .FirstOrDefault(x => x.AgentId == agentId && 
                x.TenantId == context.Tenant.Id &&
                x.AgentTypeId == (int)AgentTypes.Employee);

            if(seller is null)
                throw new Exception("Employee is invalid or could not be found");

            return seller;
        }

        public Domain.Entities.Agents.Agent? GetCustomer(Guid tenantId, Guid agentId)
        {
            if (tenantId == Guid.Empty || agentId == Guid.Empty)
                return null;

            var context = LoadContext();
            ValidateTenant(tenantId);
            var customer = _appDbContext.Agents
                .FirstOrDefault(x => x.AgentId == agentId &&
                x.TenantId == context.Tenant.Id &&
                x.AgentTypeId == (int)AgentTypes.Customer);

            if(customer is null)
                throw new Exception("Customer is invalid or could not be found");

            return customer;
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

        public AgentDetail GetAgentDetails(Guid agentId)
        {
            var agent = _appDbContext.Agents.FirstOrDefault(x => x.AgentId == agentId && x.Active);
            agent.AgentType = _appDbContext.AgentTypes.FirstOrDefault(x => x.AgentTypeId == agent.AgentTypeId);    
            return new AgentDetail
            {
                Agent = agent,
                Profile = _appDbContext.Profiles.FirstOrDefault(x => x.AgentId == agent.AgentId && x.Active),
                PhoneNumbers = _appDbContext.Phones.Where(x => x.AgentId == agentId && x.Active),
                EmailAddresses = _appDbContext.Emails.Where(x => x.AgentId == agentId && x.Active),
                Addresses = _appDbContext.Addresses.Where(x => x.AgentId == agentId && x.Active)
            };
        }

        public bool Delete(Guid tenantId, Guid AgentId)
        {
            var context = LoadContext();
            Agent agents = _appDbContext.Agents.Where(x => x.AgentId == AgentId && x.TenantId == context.Tenant.Id).First();
            agents.Active = false;

            _appDbContext.Update(agents);
            var response = _appDbContext.SaveChanges();

            if (response <= 0)
                throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            return true;
        }
    }
}
