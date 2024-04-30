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
    [Route("api/awards")]
    public class AwardsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AwardsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            var awardsDTO = await _serviceManager.AwardsService.GetAll();
            return Ok(awardsDTO);
        }

        [Route("{personId}/awards")]
        [HttpGet]
        public async Task<IActionResult> GetAwardsByPersonId(int personId)
        {
            var awardsDTO = await _serviceManager.AwardsService.GetByPersonId(personId);
            return Ok(awardsDTO);
        }

        [Route("{awardId}")]
        [HttpGet]
        public async Task<IActionResult> GetAwardById(int awardId)
        {
            var awardDTO = await _serviceManager.AwardsService.GetById(awardId);
            return Ok(awardDTO);
        }

        [Route("{personId}/awards")]
        [HttpPost]
        public async Task<IActionResult> AddAward(int personId, [FromBody] AwardForAddingDTO awardForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.AwardsService.Add(personId, awardForAddingDTO, token);
            return CreatedAtAction(nameof(GetAwardById), new { personId = response.PersonId, awardId = response.Id }, response);
        }

        [HttpDelete("{awardId}")]
        public async Task<IActionResult> DeleteAward(int awardId)
        {
            await _serviceManager.AwardsService.Delete(awardId);
            return NoContent();
        }
    }
}