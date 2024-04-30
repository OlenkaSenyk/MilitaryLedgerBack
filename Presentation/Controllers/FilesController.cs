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
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public FilesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var filesDTO = await _serviceManager.FilesService.GetAll();
            return Ok(filesDTO);
        }

        [Route("{personId}/files")]
        [HttpGet]
        public async Task<IActionResult> GetFilesByPersonId(int personId)
        {
            var filesDTO = await _serviceManager.FilesService.GetByPersonId(personId);
            return Ok(filesDTO);
        }

        [Route("{fileId}")]
        [HttpGet]
        public async Task<IActionResult> GetFileById(int fileId)
        {
            var fileDTO = await _serviceManager.FilesService.GetById(fileId);
            return Ok(fileDTO);
        }

        [Route("{personId}/files")]
        [HttpPost]
        public async Task<IActionResult> AddFile(int personId, [FromBody] FileForAddingDTO fileForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.FilesService.Add(personId, fileForAddingDTO, token);
            return CreatedAtAction(nameof(GetFileById), new { personId = response.PersonId, fileId = response.Id }, response);
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(int fileId)
        {
            await _serviceManager.FilesService.Delete(fileId);
            return NoContent();
        }
    }
}
