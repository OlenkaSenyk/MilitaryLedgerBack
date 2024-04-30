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
    [Route("api/service-histories")]
    public class ServiceHistoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ServiceHistoriesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllServiceHistories()
        {
            var servicesDTO = await _serviceManager.ServiceHistoriesService.GetAll();
            return Ok(servicesDTO);
        }

        [Route("{personId}/service-histories")]
        [HttpGet]
        public async Task<IActionResult> GetServiceHistoriesByPersonId(int personId)
        {
            var servicesDTO = await _serviceManager.ServiceHistoriesService.GetByPersonId(personId);
            return Ok(servicesDTO);
        }

        [Route("{serviceId}")]
        [HttpGet]
        public async Task<IActionResult> GetServiceHistoryById(int serviceId)
        {
            var serviceDTO = await _serviceManager.ServiceHistoriesService.GetById(serviceId);
            return Ok(serviceDTO);
        }

        [Route("{personId}/service-histories")]
        [HttpPost]
        public async Task<IActionResult> AddServiceHistory(int personId, [FromBody] ServiceHistoryForAddingDTO serviceForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.ServiceHistoriesService.Add(personId, serviceForAddingDTO, token);
            return CreatedAtAction(nameof(GetServiceHistoryById), new { personId = response.PersonId, serviceId = response.Id }, response);
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteServiceHistory(int serviceId)
        {
            await _serviceManager.ServiceHistoriesService.Delete(serviceId);
            return NoContent();
        }
    }
}
