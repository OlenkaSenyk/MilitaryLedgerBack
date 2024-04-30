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
    [Route("api/combat-participations")]
    public class CombatParticipationsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CombatParticipationsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllCombatParticipations()
        {
            var participationsDTO = await _serviceManager.CombatParticipationsService.GetAll();
            return Ok(participationsDTO);
        }

        [Route("{personId}/combat-participations")]
        [HttpGet]
        public async Task<IActionResult> GetCombatParticipationsByPersonId(int personId)
        {
            var participationsDTO = await _serviceManager.CombatParticipationsService.GetByPersonId(personId);
            return Ok(participationsDTO);
        }

        [Route("{participationId}")]
        [HttpGet]
        public async Task<IActionResult> GetCombatParticipationById(int participationId)
        {
            var participationDTO = await _serviceManager.CombatParticipationsService.GetById(participationId);
            return Ok(participationDTO);
        }

        [Route("{personId}/combat-participations")]
        [HttpPost]
        public async Task<IActionResult> AddCombatParticipation(int personId, [FromBody] CombatParticipationForAddingDTO participationForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.CombatParticipationsService.Add(personId, participationForAddingDTO, token);
            return CreatedAtAction(nameof(GetCombatParticipationById), new { personId = response.PersonId, participationId = response.Id }, response);
        }

        [HttpDelete("{participationId}")]
        public async Task<IActionResult> DeleteCombatParticipation(int participationId)
        {
            await _serviceManager.CombatParticipationsService.Delete(participationId);
            return NoContent();
        }
    }
}
