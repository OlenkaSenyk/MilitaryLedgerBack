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
    public class ServiceHistoriesService : IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceHistoriesService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ServiceHistoryDTO> Add(int personId, ServiceHistoryForAddingDTO serviceDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var service = _mapper.Map<ServiceHistory>(serviceDTO);
            service.PersonId = personId;
            service.CreatedAt = DateTime.Now;
            service.CreatedById = userId;
            service.LastUpdatedAt = DateTime.Now;
            service.LastUpdatedById = userId;
            _repositoryManager.ServiceHistoriesRepository.Add(service);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<ServiceHistoryDTO>(service);
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
            var service = await _repositoryManager.ServiceHistoriesRepository.GetById(serviceId);
            if (service is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var serviceDTO = _mapper.Map<ServiceHistoryDTO>(service);
            return serviceDTO;
        }

        public async Task<IEnumerable<ServiceHistoryDTO>> GetByPersonId(int personId)
        {
            var services = await _repositoryManager.ServiceHistoriesRepository.GetByPersonId(personId);
            return _mapper.Map<List<ServiceHistoryDTO>>(services);
        }

        public async Task<IEnumerable<ServiceHistoryDTO>> GetAll()
        {
            var service = await _repositoryManager.ServiceHistoriesRepository.GetAll();
            return _mapper.Map<List<ServiceHistoryDTO>>(service);
        }

        public async Task Update(int serviceId, ServiceHistoryDTO serviceDTO, string token)
        {
            var service = await _repositoryManager.ServiceHistoriesRepository.GetById(serviceId);
            if (service is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            service.StartDate = serviceDTO.StartDate;
            service.EndDate = serviceDTO.EndDate;
            service.MilitaryBranch = serviceDTO.MilitaryBranch;
            service.MilitaryCategory = serviceDTO.MilitaryCategory;
            service.MilitaryRank = serviceDTO.MilitaryRank;
            service.MilitaryUnit = serviceDTO.MilitaryUnit;
            service.LastUpdatedAt = DateTime.Now;
            service.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
