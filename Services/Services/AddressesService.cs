using Contracts.DTO;
using Domain.Interfaces;
using Domain.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Helpers;

namespace Services.Services
{
    public class AddressesService : IChildItemService<AddressDTO, AddressForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddressesService(IRepositoryManager repositoryManager, IMapper mapper) {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AddressDTO> Add(int personId, AddressForAddingDTO addressDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var address = _mapper.Map<Address>(addressDTO);
            address.PersonId = personId;
            address.CreatedAt = DateTime.Now;
            address.CreatedById = userId;
            address.LastUpdatedAt = DateTime.Now;
            address.LastUpdatedById = userId;
            _repositoryManager.AddressesRepository.Add(address);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<AddressDTO>(address);
        }

        public async Task Delete(int addressId)
        {
            var address = await _repositoryManager.AddressesRepository.GetById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.AddressesRepository.Delete(address);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var addresses = await _repositoryManager.AddressesRepository.GetByPersonId(personId);
            foreach (var a in addresses)
            {
                _repositoryManager.AddressesRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<AddressDTO> GetById(int addressId)
        {
            var address = await _repositoryManager.AddressesRepository.GetById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var addressDTO = _mapper.Map<AddressDTO>(address);
            return addressDTO;
        }

        public async Task<IEnumerable<AddressDTO>> GetByPersonId(int personId)
        {
            var addresses = await _repositoryManager.AddressesRepository.GetByPersonId(personId);
            return _mapper.Map<List<AddressDTO>>(addresses);
        }

        public async Task<IEnumerable<AddressDTO>> GetAll()
        {
            var address = await _repositoryManager.AddressesRepository.GetAll();
            return _mapper.Map<List<AddressDTO>>(address);
        }

        public async Task Update(int addressId, AddressDTO addressDTO, string token)
        {
            var address = await _repositoryManager.AddressesRepository.GetById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            address.Country = addressDTO.Country;
            address.City = addressDTO.City;
            address.Region = addressDTO.Region;
            address.Street = addressDTO.Street;
            address.House = addressDTO.House;
            address.Entrance = addressDTO.Entrance;
            address.Apartment = addressDTO.Apartment;
            address.ResidenceOrRegistration = addressDTO.ResidenceOrRegistration;
            address.LastUpdatedAt = DateTime.Now;
            address.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
