using Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AddressesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addressesDTO = await _serviceManager.AddressesService.GetAll();
            return Ok(addressesDTO);
        }

        [Route("{personId}/addresses")]
        [HttpGet]
        public async Task<IActionResult> GetAddressesByPersonId(int personId)
        {
            var addressesDTO = await _serviceManager.AddressesService.GetByPersonId(personId);
            return Ok(addressesDTO);
        }

        [Route("{addressId}")]
        [HttpGet]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var addressDTO = await _serviceManager.AddressesService.GetById(addressId);
            return Ok(addressDTO);
        }

        [Route("{personId}/addresses")]
        [HttpPost]
        public async Task<IActionResult> AddAddress(int personId, [FromBody] AddressForAddingDTO addressForAddingDTO)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var response = await _serviceManager.AddressesService.Add(personId, addressForAddingDTO, token);
            return CreatedAtAction(nameof(GetAddressById), new { personId = response.PersonId, addressId = response.Id }, response);
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            await _serviceManager.AddressesService.Delete(addressId);
            return NoContent();
        }
    }
}
