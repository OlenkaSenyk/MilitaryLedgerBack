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
    [Route("api/analysis")]
    public class AnalysisController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AnalysisController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [Route("marital-status")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByMaritalStatus()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.MaritalStatus);
            return Ok(result);
        }

        [Route("education-level")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByEducationLevelStatus()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.EducationLevel);
            return Ok(result);
        }

        [Route("sex")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticBySex()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.Sex);
            return Ok(result);
        }

        [Route("nationality")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByNationality()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.Nationality);
            return Ok(result);
        }

        [Route("education")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByEducation()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.Education);
            return Ok(result);
        }

        [Route("need-mmc")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByNeedingMMC()
        {
            var people = await _serviceManager.PeopleService.GetAllPeople();
            var result = await _serviceManager.AnalysisService.GetStatisticByPersonField(people, p => p.NeedMMC.ToString());
            return Ok(result);
        }

        [Route("military-rank")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticByMilitaryRank()
        {
            var service = await _serviceManager.ServiceHistoriesService.GetAll();
            var result = await _serviceManager.AnalysisService.GetStatisticByServiceHistoriesField(service, p => p.MilitaryRank);
            return Ok(result);
        }     
    }
}
