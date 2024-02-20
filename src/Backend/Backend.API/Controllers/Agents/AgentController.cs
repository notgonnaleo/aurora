using Backend.Domain.Entities.Agent;
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
        public ActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_agentService.GetById(id));
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
                var Agents = _agentService.Update(model);

                return Ok(Agents);

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