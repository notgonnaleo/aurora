using Backend.Domain.Entities.Agents;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Agents
{
    public class AgentTypeService : Service
    {
        private readonly AppDbContext _appDbContext;
        public AgentTypeService(AppDbContext appDbContext, UserContextService main) : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<AgentType> GetAgentTypes()
        {
            return _appDbContext.AgentTypes;
        }
    }
}
