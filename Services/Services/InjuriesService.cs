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
    public class InjuriesService: IChildItemService<InjurieDTO, InjurieForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public InjuriesService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<InjurieDTO> Add(int personId, InjurieForAddingDTO injurieDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var injurie = _mapper.Map<Injurie>(injurieDTO);
            injurie.PersonId = personId;
            injurie.CreatedAt = DateTime.Now;
            injurie.CreatedById = userId;
            injurie.LastUpdatedAt = DateTime.Now;
            injurie.LastUpdatedById = userId;
            _repositoryManager.InjuriesRepository.Add(injurie);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<InjurieDTO>(injurie);
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
            var injurieDTO = _mapper.Map<InjurieDTO>(injurie);
            return injurieDTO;
        }

        public async Task<IEnumerable<InjurieDTO>> GetByPersonId(int personId)
        {
            var injuries = await _repositoryManager.InjuriesRepository.GetByPersonId(personId);
            return _mapper.Map<List<InjurieDTO>>(injuries);
        }

        public async Task<IEnumerable<InjurieDTO>> GetAll()
        {
            var injuries = await _repositoryManager.InjuriesRepository.GetAll();
            return _mapper.Map<List<InjurieDTO>>(injuries);
        }

        public async Task Update(int injurieId, InjurieDTO injurieDTO, string token)
        {
            var injurie = await _repositoryManager.InjuriesRepository.GetById(injurieId);
            if (injurie is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            injurie.Location = injurieDTO.Location;
            injurie.Date = injurieDTO.Date;
            injurie.Notes = injurieDTO.Notes;
            injurie.MedicalAssistance = injurieDTO.MedicalAssistance;
            injurie.Type = injurieDTO.Type;
            injurie.LastUpdatedAt = DateTime.Now;
            injurie.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
