using AutoMapper;
using Contracts.DTO;
using Contracts.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private string _secretKey;

        public LoginService(IRepositoryManager repositoryManager, IMapper mapper, string secretKey)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _secretKey = secretKey;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var user = await _repositoryManager.UsersRepository.GetUserByEmail(request.Email);

            if (user == null || !SecurityHelper.VerifyHash(request.Password, user.Password))
            {
                return null;
            }

            string token = SecurityHelper.CreateToken(_secretKey, user.Id.ToString(), user.Role);
            LoginResponseDTO response = _mapper.Map<LoginResponseDTO>(user);
            response.Token = token;

            return response;
        }

        public async Task<RegistrationRequestDTO> Register(RegistrationRequestDTO request, string token)
        {
            var user = await _repositoryManager.UsersRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                //throw new UserAlreadyExist(userId);
            }

            var newUser = _mapper.Map<User>(request);

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
