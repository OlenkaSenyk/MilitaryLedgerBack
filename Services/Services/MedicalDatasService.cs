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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Services
{
    public class MedicalDatasService : IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public MedicalDatasService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<MedicalDataDTO> Add(int personId, MedicalDataForAddingDTO dataDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var data = _mapper.Map<MedicalData>(dataDTO);
            data.PersonId = personId;
            data.CreatedAt = DateTime.Now;
            data.CreatedById = userId;
            data.LastUpdatedAt = DateTime.Now;
            data.LastUpdatedById = userId;
            _repositoryManager.MedicalDatasRepository.Add(data);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<MedicalDataDTO>(data);
        }

        public async Task Delete(int dataId)
        {
            var data = await _repositoryManager.MedicalDatasRepository.GetById(dataId);
            if (data is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.MedicalDatasRepository.Delete(data);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var datas = await _repositoryManager.MedicalDatasRepository.GetByPersonId(personId);
            foreach (var a in datas)
            {
                _repositoryManager.MedicalDatasRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<MedicalDataDTO> GetById(int dataId)
        {
            var data = await _repositoryManager.MedicalDatasRepository.GetById(dataId);
            if (data is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var dataDTO = _mapper.Map<MedicalDataDTO>(data);
            return dataDTO;
        }

        public async Task<IEnumerable<MedicalDataDTO>> GetByPersonId(int personId)
        {
            var datas = await _repositoryManager.MedicalDatasRepository.GetByPersonId(personId);
            return _mapper.Map<List<MedicalDataDTO>>(datas);
        }

        public async Task<IEnumerable<MedicalDataDTO>> GetAll()
        {
            var data = await _repositoryManager.MedicalDatasRepository.GetAll();
            return _mapper.Map<List<MedicalDataDTO>>(data);
        }

        public async Task Update(int dataId, MedicalDataDTO dataDTO, string token)
        {
            var data = await _repositoryManager.MedicalDatasRepository.GetById(dataId);
            if (data is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            data.BloodType = dataDTO.BloodType;
            data.BloodRh = dataDTO.BloodRh;
            data.Eligibility = dataDTO.Eligibility;
            data.Features = dataDTO.Features;
            data.Notes = dataDTO.Notes;
            data.LastUpdatedAt = DateTime.Now;
            data.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
