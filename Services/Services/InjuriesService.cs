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
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class InjuriesService: IChildItemService<InjurieDTO, InjurieForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public InjuriesService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<InjurieDTO> Add(int personId, InjurieForAddingDTO injurieDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var injurie = new Injurie();
            string[] fields = SecurityHelper.GetAllFieldsNames(injurieDTO);
            SecurityHelper.ProtectFields(injurieDTO, injurie, fields, _protector.Protect);

            injurie.PersonId = personId;
            injurie.CreatedAt = DateTime.Now;
            injurie.CreatedById = userId;
            injurie.LastUpdatedAt = DateTime.Now;
            injurie.LastUpdatedById = userId;
            _repositoryManager.InjuriesRepository.Add(injurie);

            await _repositoryManager.UnitOfWork.SaveChanges();

            var injurieForReturn = _mapper.Map<InjurieDTO>(injurieDTO);
            injurieForReturn.Id = injurie.Id;
            injurieForReturn.PersonId = personId;
            return injurieForReturn;
        }

        public async Task Delete(int injurieId)
        {
            var injurie = await _repositoryManager.InjuriesRepository.GetById(injurieId);
            if (injurie is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.InjuriesRepository.Delete(injurie);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var injuries = await _repositoryManager.InjuriesRepository.GetByPersonId(personId);
            foreach (var a in injuries)
            {
                _repositoryManager.InjuriesRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<InjurieDTO> GetById(int injurieId)
        {
            var injurie = await _repositoryManager.InjuriesRepository.GetById(injurieId);
            if (injurie is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var injurieDTO = new InjurieDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new InjurieForAddingDTO());
            SecurityHelper.UnprotectFields(injurie, injurieDTO, fields, _protector.Unprotect);
            injurieDTO.Id = injurie.Id;
            injurieDTO.PersonId = injurie.PersonId;
            return injurieDTO;
        }

        public async Task<IEnumerable<InjurieDTO>> GetByPersonId(int personId)
        {
            var injuries = await _repositoryManager.InjuriesRepository.GetByPersonId(personId);
            if (injuries == null || !injuries.Any())
            {
                return Enumerable.Empty<InjurieDTO>();
            }

            var injurieDTOs = new List<InjurieDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new InjurieForAddingDTO());
            foreach (var injurie in injuries)
            {
                var injurieDTO = new InjurieDTO();
                SecurityHelper.UnprotectFields(injurie, injurieDTO, fields, _protector.Unprotect);
                injurieDTO.Id = injurie.Id;
                injurieDTO.PersonId = injurie.PersonId;
                injurieDTOs.Add(injurieDTO);
            }

            return injurieDTOs;
        }

        public async Task<IEnumerable<InjurieDTO>> GetAll()
        {
            var injuries = await _repositoryManager.InjuriesRepository.GetAll();
            if (injuries == null || !injuries.Any())
            {
                return Enumerable.Empty<InjurieDTO>();
            }

            var injurieDTOs = new List<InjurieDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new InjurieForAddingDTO());
            foreach (var injurie in injuries)
            {
                var injurieDTO = new InjurieDTO();
                SecurityHelper.UnprotectFields(injurie, injurieDTO, fields, _protector.Unprotect);
                injurieDTO.Id = injurie.Id;
                injurieDTO.PersonId = injurie.PersonId;
                injurieDTOs.Add(injurieDTO);
            }

            return injurieDTOs;
        }
    }
}
