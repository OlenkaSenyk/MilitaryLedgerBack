using Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<RegistrationRequestDTO> Register(RegistrationRequestDTO request, string token);
    }
}
