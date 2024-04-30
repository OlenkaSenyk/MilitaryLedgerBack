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
    public class CombatParticipationsService : IChildItemService<CombatParticipationDTO, CombatParticipationForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CombatParticipationsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<CombatParticipationDTO> Add(int personId, CombatParticipationForAddingDTO participationDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var participation = _mapper.Map<CombatParticipation>(participationDTO);
            participation.PersonId = personId;
            participation.CreatedAt = DateTime.Now;
            participation.CreatedById = userId;
            participation.LastUpdatedAt = DateTime.Now;
            participation.LastUpdatedById = userId;
            _repositoryManager.CombatParticipationsRepository.Add(participation);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<CombatParticipationDTO>(participation);
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
            var participationDTO = _mapper.Map<CombatParticipationDTO>(participation);
            return participationDTO;
        }

        public async Task<IEnumerable<CombatParticipationDTO>> GetByPersonId(int personId)
        {
            var participations = await _repositoryManager.CombatParticipationsRepository.GetByPersonId(personId);
            return _mapper.Map<List<CombatParticipationDTO>>(participations);
        }

        public async Task<IEnumerable<CombatParticipationDTO>> GetAll()
        {
            var participations = await _repositoryManager.CombatParticipationsRepository.GetAll();
            return _mapper.Map<List<CombatParticipationDTO>>(participations);
        }

        public async Task Update(int participationId, CombatParticipationDTO participationDTO, string token)
        {
            var participation = await _repositoryManager.CombatParticipationsRepository.GetById(participationId);
            if (participation is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            participation.StartDate = participationDTO.StartDate;
            participation.EndDate = participationDTO.EndDate;
            participation.Location = participationDTO.Location;
            participation.OperationType = participationDTO.OperationType;
            participation.LastUpdatedAt = DateTime.Now;
            participation.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
