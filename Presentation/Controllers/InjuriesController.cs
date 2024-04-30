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
    [Route("api/injuries")]
    public class InjuriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public InjuriesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllInjuries()
        {
            var injuriesDTO = await _serviceManager.InjuriesService.GetAll();
            return Ok(injuriesDTO);
        }

        [Route("{personId}/injuries")]
        [HttpGet]
        public async Task<IActionResult> GetInjuriesByPersonId(int personId)
        {
            var injuriesDTO = await _serviceManager.InjuriesService.GetByPersonId(personId);
            return Ok(injuriesDTO);
        }

        [Route("{injurieId}")]
        [HttpGet]
        public async Task<IActionResult> GetInjurieById(int injurieId)
        {
            var injurieDTO = await _serviceManager.InjuriesService.GetById(injurieId);
            return Ok(injurieDTO);
        }

        [Route("{personId}/injuries")]
        [HttpPost]
        public async Task<IActionResult> AddInjurie(int personId, [FromBody] InjurieForAddingDTO injurieForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.InjuriesService.Add(personId, injurieForAddingDTO, token);
            return CreatedAtAction(nameof(GetInjurieById), new { personId = response.PersonId, injurieId = response.Id }, response);
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteInjurie(int injurieId)
        {
            await _serviceManager.InjuriesService.Delete(injurieId);
            return NoContent();
        }
    }
}
