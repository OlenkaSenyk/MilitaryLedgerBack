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
    [Route("api/medical-datas")]
    public class MedicalDatasController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public MedicalDatasController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllMedicalDatas()
        {
            var datasDTO = await _serviceManager.MedicalDatasService.GetAll();
            return Ok(datasDTO);
        }

        [Route("{personId}/medical-datas")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalDatasByPersonId(int personId)
        {
            var dataDTO = await _serviceManager.MedicalDatasService.GetByPersonId(personId);
            return Ok(dataDTO);
        }

        [Route("{dataId}")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalDataById(int dataId)
        {
            var dataDTO = await _serviceManager.MedicalDatasService.GetById(dataId);
            return Ok(dataDTO);
        }

        [Route("{personId}/medical-datas")]
        [HttpPost]
        public async Task<IActionResult> AddMedicalData(int personId, [FromBody] MedicalDataForAddingDTO dataForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.MedicalDatasService.Add(personId, dataForAddingDTO, token);
            return CreatedAtAction(nameof(GetMedicalDataById), new { personId = response.PersonId, dataId = response.Id }, response);
        }

        [HttpDelete("{dataId}")]
        public async Task<IActionResult> DeleteMedicalData(int dataId)
        {
            await _serviceManager.MedicalDatasService.Delete(dataId);
            return NoContent();
        }
    }
}
