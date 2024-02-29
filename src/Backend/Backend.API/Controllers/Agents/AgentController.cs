using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Agents
{
    [ApiController]
    [Route("Agents")]
    public class AgentController : ControllerBase
    {
        private readonly AgentService _agentService;
        private readonly UserContextService _userContextService;

        public AgentController(AgentService agentService, UserContextService userContextService)
        {
            _agentService = agentService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(Agent agent)
        {
            try
            {
                return Ok(_agentService.Add(agent));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get()
        {
            try
            {
                return Ok(_agentService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid tenantId,Guid agentId)
        {
            try
            {
                return Ok(_agentService.GetById(tenantId,agentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Agent model)
        {
            try
            {
                
                return Ok(_agentService.Update(model));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_agentService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}