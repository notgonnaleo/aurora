using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Agents
{
    [ApiController]
    [Route("Agents")]
    public class AgentsController : ControllerBase
    {
        private readonly AgentService _agentService;
        private readonly UserContextService _userContextService;

        public AgentsController(AgentService agentService, UserContextService userContextService)
        {
            _agentService = agentService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Agent agent)
        {
            try
            {
                return Ok(await _agentService.Add(agent));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get(Guid tenantId)
        {
            try
            {
                return Ok(await _agentService.Get(tenantId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid Id, Guid tenantId)
        {
            try
            {
                return Ok(await _agentService.GetById(Id, tenantId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(Guid Id,Agent agents,Guid tenantId)
        {
            try
            {
                var Agents = await _agentService.Update(Id, agents,tenantId);

                return Ok(Agents);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(Guid Id, Guid tenantId)
        {
            try
            {
                return Ok(await _agentService.Delete(Id,tenantId));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}