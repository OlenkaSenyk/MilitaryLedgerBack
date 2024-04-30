using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UsersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var response = await _serviceManager.LoginService.Login(request);

            if (response == null)
            {
                //_apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //_apiResponse.IsSuccess = false;
                //_apiResponse.ErrorMsgs.Add("Email or password is incorrect");
                return BadRequest(response);
            }

            //_apiResponse.StatusCode = HttpStatusCode.OK;
            //_apiResponse.IsSuccess = true;
           // _apiResponse.Result = response;
            return Ok(response);
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            string token;
            string tokenHeader = Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(tokenHeader))
            {
                string[] parts = tokenHeader.Split(' ');
                if (parts.Length == 2 && parts[0].Equals("Bearer"))
                {
                    token = parts[1];
                }
                else
                {
                    token = null;
                }
            }
            else
            {
                token = null;
            }

            var user = await _serviceManager.LoginService.Register(request, token);

            if (user == null)
            {
                //_apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //_apiResponse.IsSuccess = false;
                //_apiResponse.ErrorMsgs.Add("Something went wrong during registration");
                return BadRequest(user);
            }

            //_apiResponse.StatusCode = HttpStatusCode.OK;
            //_apiResponse.IsSuccess = true;
            //_apiResponse.Result = user;
            return Ok(user);
        }
    }
}
