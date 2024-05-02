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
    public class ServiceHistoriesService : IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public ServiceHistoriesService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<ServiceHistoryDTO> Add(int personId, ServiceHistoryForAddingDTO serviceDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var service = new ServiceHistory();
            string[] fields = SecurityHelper.GetAllFieldsNames(serviceDTO);
            SecurityHelper.ProtectFields(serviceDTO, service, fields, _protector.Protect);

            service.PersonId = personId;
            service.CreatedAt = DateTime.Now;
            service.CreatedById = userId;
            service.LastUpdatedAt = DateTime.Now;
            service.LastUpdatedById = userId;
            _repositoryManager.ServiceHistoriesRepository.Add(service);

            await _repositoryManager.UnitOfWork.SaveChanges();

            var serviceForReturn = _mapper.Map<ServiceHistoryDTO>(serviceDTO);
            serviceForReturn.Id = service.Id;
            serviceForReturn.PersonId = personId;
            return serviceForReturn;
        }

        public async Task Delete(int serviceId)
        {
            var service = await _repositoryManager.ServiceHistoriesRepository.GetById(serviceId);
            if (service is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.ServiceHistoriesRepository.Delete(service);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var services = await _repositoryManager.ServiceHistoriesRepository.GetByPersonId(personId);
            foreach (var a in services)
            {
                _repositoryManager.ServiceHistoriesRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<ServiceHistoryDTO> GetById(int serviceId)
        {
            var service = await _repositoryManager.AddressesRepository.GetById(serviceId);
            if (service is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var serviceDTO = new ServiceHistoryDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new ServiceHistoryForAddingDTO());
            SecurityHelper.UnprotectFields(service, serviceDTO, fields, _protector.Unprotect);
            serviceDTO.Id = service.Id;
            serviceDTO.PersonId = service.PersonId;
            return serviceDTO;
        }

        public async Task<IEnumerable<ServiceHistoryDTO>> GetByPersonId(int personId)
        {
            var services = await _repositoryManager.ServiceHistoriesRepository.GetByPersonId(personId);
            if (services == null || !services.Any())
            {
                return Enumerable.Empty<ServiceHistoryDTO>();
            }

            var serviceDTOs = new List<ServiceHistoryDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new ServiceHistoryForAddingDTO());
            foreach (var service in services)
            {
                var serviceDTO = new ServiceHistoryDTO();
                SecurityHelper.UnprotectFields(service, serviceDTO, fields, _protector.Unprotect);
                serviceDTO.Id = service.Id;
                serviceDTO.PersonId = service.PersonId;
                serviceDTOs.Add(serviceDTO);
            }

            return serviceDTOs;
        }

        public async Task<IEnumerable<ServiceHistoryDTO>> GetAll()
        {
            var services = await _repositoryManager.ServiceHistoriesRepository.GetAll();
            if (services == null || !services.Any())
            {
                return Enumerable.Empty<ServiceHistoryDTO>();
            }

            var serviceDTOs = new List<ServiceHistoryDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new ServiceHistoryForAddingDTO());
            foreach (var service in services)
            {
                var serviceDTO = new ServiceHistoryDTO();
                SecurityHelper.UnprotectFields(service, serviceDTO, fields, _protector.Unprotect);
                serviceDTO.Id = service.Id;
                serviceDTO.PersonId = service.PersonId;
                serviceDTOs.Add(serviceDTO);
            }

            return serviceDTOs;
        }
    }
}
