using Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public PeopleController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            return Ok(people);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonById(int personId)
        {
            var personDTO = await _serviceManager.PeopleService.GetPersonById(personId);
            return Ok(personDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonForAddingDTO personForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var personDTO = await _serviceManager.PeopleService.Add(personForAddingDTO, token);
            return CreatedAtAction(nameof(GetPersonById), new { personId = personDTO.Id }, personDTO);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePerson(int personId, [FromBody] PersonDTO personDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            await _serviceManager.PeopleService.Update(personId, personDTO, token);
            return NoContent();
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            await _serviceManager.PeopleService.Delete(personId);
            return NoContent();
        }
    }
}
