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
    public class CombatParticipationsService : IChildItemService<CombatParticipationDTO, CombatParticipationForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public CombatParticipationsService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<CombatParticipationDTO> Add(int personId, CombatParticipationForAddingDTO participationDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var participation = new CombatParticipation();
            string[] fields = SecurityHelper.GetAllFieldsNames(participationDTO);
            SecurityHelper.ProtectFields(participationDTO, participation, fields, _protector.Protect);

            participation.PersonId = personId;
            participation.CreatedAt = DateTime.Now;
            participation.CreatedById = userId;
            participation.LastUpdatedAt = DateTime.Now;
            participation.LastUpdatedById = userId;
            _repositoryManager.CombatParticipationsRepository.Add(participation);
            await _repositoryManager.UnitOfWork.SaveChanges();

            var participationForReturn = _mapper.Map<CombatParticipationDTO>(participationDTO);
            participationForReturn.Id = participation.Id;
            participationForReturn.PersonId = personId;
            return participationForReturn;
        }

        public async Task Delete(int participationId)
        {
            var participation = await _repositoryManager.CombatParticipationsRepository.GetById(participationId);
            if (participation is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.CombatParticipationsRepository.Delete(participation);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var participations = await _repositoryManager.CombatParticipationsRepository.GetByPersonId(personId);
            foreach (var a in participations)
            {
                _repositoryManager.CombatParticipationsRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<CombatParticipationDTO> GetById(int participationId)
        {
            var participation = await _repositoryManager.CombatParticipationsRepository.GetById(participationId);
            if (participation is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var participationDTO = new CombatParticipationDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new CombatParticipationForAddingDTO());
            SecurityHelper.UnprotectFields(participation, participationDTO, fields, _protector.Unprotect);
            participationDTO.Id = participation.Id;
            participationDTO.PersonId = participation.PersonId;
            return participationDTO;
        }

        public async Task<IEnumerable<CombatParticipationDTO>> GetByPersonId(int personId)
        {
            var participations = await _repositoryManager.CombatParticipationsRepository.GetByPersonId(personId);
            if (participations == null || !participations.Any())
            {
                return Enumerable.Empty<CombatParticipationDTO>();
            }

            var participationDTOs = new List<CombatParticipationDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new CombatParticipationForAddingDTO());
            foreach (var participation in participations)
            {
                var participationDTO = new CombatParticipationDTO();
                SecurityHelper.UnprotectFields(participation, participationDTO, fields, _protector.Unprotect);
                participationDTO.Id = participation.Id;
                participationDTO.PersonId = participation.PersonId;
                participationDTOs.Add(participationDTO);
            }

            return participationDTOs;
        }

        public async Task<IEnumerable<CombatParticipationDTO>> GetAll()
        {
            var participations = await _repositoryManager.CombatParticipationsRepository.GetAll();
            if (participations == null || !participations.Any())
            {
                return Enumerable.Empty<CombatParticipationDTO>();
            }

            var participationDTOs = new List<CombatParticipationDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new CombatParticipationForAddingDTO());
            foreach (var participation in participations)
            {
                var participationDTO = new CombatParticipationDTO();
                SecurityHelper.UnprotectFields(participation, participationDTO, fields, _protector.Unprotect);
                participationDTO.Id = participation.Id;
                participationDTO.PersonId = participation.PersonId;
                participationDTOs.Add(participationDTO);
            }

            return participationDTOs;
        }
    }
}
