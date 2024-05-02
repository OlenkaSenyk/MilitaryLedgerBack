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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Services
{
    public class MedicalDatasService : IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public MedicalDatasService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<MedicalDataDTO> Add(int personId, MedicalDataForAddingDTO dataDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var data = new MedicalData();
            string[] fields = SecurityHelper.GetAllFieldsNames(dataDTO);
            SecurityHelper.ProtectFields(dataDTO, data, fields, _protector.Protect);

            data.PersonId = personId;
            data.CreatedAt = DateTime.Now;
            data.CreatedById = userId;
            data.LastUpdatedAt = DateTime.Now;
            data.LastUpdatedById = userId;
            _repositoryManager.MedicalDatasRepository.Add(data);

            await _repositoryManager.UnitOfWork.SaveChanges();

            var dataForReturn = _mapper.Map<MedicalDataDTO>(dataDTO);
            dataForReturn.Id = data.Id;
            dataForReturn.PersonId = personId;
            return dataForReturn;
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
            var dataDTO = new MedicalDataDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new MedicalDataForAddingDTO());
            SecurityHelper.UnprotectFields(data, dataDTO, fields, _protector.Unprotect);
            dataDTO.Id = data.Id;
            dataDTO.PersonId = data.PersonId;
            return dataDTO;
        }

        public async Task<IEnumerable<MedicalDataDTO>> GetByPersonId(int personId)
        {
            var datas = await _repositoryManager.MedicalDatasRepository.GetByPersonId(personId);
            if (datas == null || !datas.Any())
            {
                return Enumerable.Empty<MedicalDataDTO>();
            }

            var dataDTOs = new List<MedicalDataDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new MedicalDataForAddingDTO());
            foreach (var data in datas)
            {
                var dataDTO = new MedicalDataDTO();
                SecurityHelper.UnprotectFields(data, dataDTO, fields, _protector.Unprotect);
                dataDTO.Id = data.Id;
                dataDTO.PersonId = data.PersonId;
                dataDTOs.Add(dataDTO);
            }

            return dataDTOs;
        }

        public async Task<IEnumerable<MedicalDataDTO>> GetAll()
        {
            var datas = await _repositoryManager.MedicalDatasRepository.GetAll();
            if (datas == null || !datas.Any())
            {
                return Enumerable.Empty<MedicalDataDTO>();
            }

            var dataDTOs = new List<MedicalDataDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new MedicalDataForAddingDTO());
            foreach (var data in datas)
            {
                var dataDTO = new MedicalDataDTO();
                SecurityHelper.UnprotectFields(data, dataDTO, fields, _protector.Unprotect);
                dataDTO.Id = data.Id;
                dataDTO.PersonId = data.PersonId;
                dataDTOs.Add(dataDTO);
            }

            return dataDTOs;
        }
    }
}
