using AutoMapper;
using Contracts.DTO;
using Contracts.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private string _secretKey;
        private readonly IDataProtector _protector;
        private readonly string[] fields;

        public LoginService(IRepositoryManager repositoryManager, IMapper mapper, string secretKey, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _secretKey = secretKey;
            _protector = provider.CreateProtector(GetType().Name);
            fields = new string[] { "FirstName", "LastName", "MiddleName", "Password", "Phone", "Role" };
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var user = await _repositoryManager.UsersRepository.GetUserByEmail(request.Email);
            var tempPassword = _protector.Unprotect(user.Password);
            if (user == null || !SecurityHelper.VerifyHash(request.Password, tempPassword))
            {
                return null;
            }
            var role = _protector.Unprotect(user.Role);
            string token = SecurityHelper.CreateToken(_secretKey, user.Id.ToString(), role);
            LoginResponseDTO response = new LoginResponseDTO();
            SecurityHelper.ProtectFields(user, response, fields, _protector.Unprotect);
            response.Token = token;
            response.Id = user.Id;
            response.Email = user.Email;

            return response;
        }

        public async Task<RegistrationRequestDTO> Register(RegistrationRequestDTO request, string token)
        {
            var user = await _repositoryManager.UsersRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                return null;
                //throw new UserAlreadyExist(userId);
            }

            request.Password = SecurityHelper.GetHashedString(request.Password);
            var newUser = new User();
            newUser.Email = request.Email;
            SecurityHelper.ProtectFields(request, newUser, fields, _protector.Protect);

            if (token != null)
            {
                int userId = SecurityHelper.GetClaimsFromToken(token);
                newUser.CreatedById = userId;
                newUser.LastUpdatedById = userId;
            }

            newUser.CreatedAt = DateTime.Now;
            newUser.LastUpdatedAt = DateTime.Now;

            _repositoryManager.UsersRepository.Add(newUser);
            await _repositoryManager.UnitOfWork.SaveChanges();

            if (token == null)
            {
                var userUp = await _repositoryManager.UsersRepository.GetUserByEmail(request.Email);
                userUp.CreatedById = userUp.Id;
                userUp.LastUpdatedById = userUp.Id;
                await _repositoryManager.UnitOfWork.SaveChanges();
            }

            return _mapper.Map<RegistrationRequestDTO>(newUser);
        }
    }
}
