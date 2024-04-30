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
    public class AwardsService : IChildItemService<AwardDTO, AwardForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AwardsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AwardDTO> Add(int personId, AwardForAddingDTO awardDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var award = _mapper.Map<Award>(awardDTO);
            award.PersonId = personId;
            award.CreatedAt = DateTime.Now;
            award.CreatedById = userId;
            award.LastUpdatedAt = DateTime.Now;
            award.LastUpdatedById = userId;
            _repositoryManager.AwardsRepository.Add(award);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<AwardDTO>(award);
        }

        public async Task Delete(int awardId)
        {
            var award = await _repositoryManager.AwardsRepository.GetById(awardId);
            if (award is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.AwardsRepository.Delete(award);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var awards = await _repositoryManager.AwardsRepository.GetByPersonId(personId);
            foreach (var a in awards)
            {
                _repositoryManager.AwardsRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<AwardDTO> GetById(int awardId)
        {
            var award = await _repositoryManager.AwardsRepository.GetById(awardId);
            if (award is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var awardDTO = _mapper.Map<AwardDTO>(award);
            return awardDTO;
        }

        public async Task<IEnumerable<AwardDTO>> GetByPersonId(int personId)
        {
            var awards = await _repositoryManager.AwardsRepository.GetByPersonId(personId);
            return _mapper.Map<List<AwardDTO>>(awards);
        }

        public async Task<IEnumerable<AwardDTO>> GetAll()
        {
            var award = await _repositoryManager.AwardsRepository.GetAll();
            return _mapper.Map<List<AwardDTO>>(award);
        }

        public async Task Update(int awardId, AwardDTO awardDTO, string token)
        {
            var award = await _repositoryManager.AwardsRepository.GetById(awardId);
            if (award is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            award.Name = awardDTO.Name;
            award.Date = awardDTO.Date;
            award.Reason = awardDTO.Reason;
            award.LastUpdatedAt = DateTime.Now;
            award.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
