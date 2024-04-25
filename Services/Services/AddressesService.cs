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

namespace Services.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IRepositoryManager _repositoryManager;
        public AddressesService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<AddressDTO> Add(int personId, AddressForAddingDTO addressDTO)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            var address = new Address
            {
                Country = addressDTO.Country,
                City = addressDTO.City,
                Region = addressDTO.Region,
                Street = addressDTO.Street,
                House = addressDTO.House,
                Entrance = addressDTO.Entrance,
                Apartment = addressDTO.Apartment,
                ResidenceOrRegistration = addressDTO.ResidenceOrRegistration,
                PersonId = personId,
            };
            _repositoryManager.AddressesRepository.Add(address);
            await _repositoryManager.UnitOfWork.SaveChanges();
            return new AddressDTO
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                Region = address.Region,
                Street = address.Street,
                House = address.House,
                Entrance = address.Entrance,
                Apartment = address.Apartment,
                ResidenceOrRegistration = address.ResidenceOrRegistration,
                PersonId = personId,
            };
        }

        public async Task Delete(int addressId)
        {
            var address = await _repositoryManager.AddressesRepository.GetAddressById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.AddressesRepository.Delete(address);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var addresses = await _repositoryManager.AddressesRepository.GetAddressesByPersonId(personId);
            foreach (var a in addresses)
            {
                _repositoryManager.AddressesRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<AddressDTO> GetAddressById(int addressId)
        {
            var address = await _repositoryManager.AddressesRepository.GetAddressById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var addressDTO = new AddressDTO
            {
                Id = addressId,
                Country = address.Country,
                City = address.City,
                Region = address.Region,
                Street = address.Street,
                House = address.House,
                Entrance = address.Entrance,
                Apartment = address.Apartment,
                ResidenceOrRegistration = address.ResidenceOrRegistration,
                PersonId = address.PersonId
            };
            return addressDTO;
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesByPersonId(int personId)
        {
            var addresses = await _repositoryManager.AddressesRepository.GetAddressesByPersonId(personId);
            var addressesDTO = addresses.Select(a => new AddressDTO
            {
                Id = a.Id,
                Country = a.Country,
                City = a.City,
                Region = a.Region,
                Street = a.Street,
                House = a.House,
                Entrance = a.Entrance,
                Apartment = a.Apartment,
                ResidenceOrRegistration = a.ResidenceOrRegistration,
                PersonId = a.PersonId
            }).ToList();
            return addressesDTO;
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAddresses()
        {
            var address = await _repositoryManager.AddressesRepository.GetAllAddresses();
            return address.Select(a => new AddressDTO
            {
                Id = a.Id,
                Country = a.Country,
                City = a.City,
                Region = a.Region,
                Street = a.Street,
                House = a.House,
                Entrance = a.Entrance,
                Apartment = a.Apartment,
                ResidenceOrRegistration = a.ResidenceOrRegistration,
                PersonId = a.PersonId
            });
        }

        public async Task Update(int addressId, AddressDTO addressDTO)
        {
            var address = await _repositoryManager.AddressesRepository.GetAddressById(addressId);
            if (address is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            address.Country = addressDTO.Country;
            address.City = addressDTO.City;
            address.Region = addressDTO.Region;
            address.Street = addressDTO.Street;
            address.House = addressDTO.House;
            address.Entrance = addressDTO.Entrance;
            address.Apartment = addressDTO.Apartment;
            address.ResidenceOrRegistration = addressDTO.ResidenceOrRegistration;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
