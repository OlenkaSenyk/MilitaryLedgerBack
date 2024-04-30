using Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/parameters")]
    public class ParametersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ParametersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllParameters()
        {
            var parametersDTO = await _serviceManager.ParametersService.GetAll();
            return Ok(parametersDTO);
        }

        [Route("{personId}/parameters")]
        [HttpGet]
        public async Task<IActionResult> GetParametersByPersonId(int personId)
        {
            var parametersDTO = await _serviceManager.ParametersService.GetByPersonId(personId);
            return Ok(parametersDTO);
        }

        [Route("{parameterId}")]
        [HttpGet]
        public async Task<IActionResult> GetParametersById(int parameterId)
        {
            var parameterDTO = await _serviceManager.ParametersService.GetById(parameterId);
            return Ok(parameterDTO);
        }

        [Route("{personId}/parameters")]
        [HttpPost]
        public async Task<IActionResult> AddParameter(int personId, [FromBody] ParameterForAddingDTO parameterForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.ParametersService.Add(personId, parameterForAddingDTO, token);
            return CreatedAtAction(nameof(GetParametersById), new { personId = response.PersonId, parameterId = response.Id }, response);
        }

        [HttpDelete("{parameterId}")]
        public async Task<IActionResult> DeleteParameter(int parameterId)
        {
            await _serviceManager.ParametersService.Delete(parameterId);
            return NoContent();
        }
    }
}
