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
using Microsoft.AspNetCore.DataProtection;

namespace Services.Services
{
    public class AddressesService : IChildItemService<AddressDTO, AddressForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public AddressesService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider) {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<AddressDTO> Add(int personId, AddressForAddingDTO addressDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var address = new Address();
            string[] fields = SecurityHelper.GetAllFieldsNames(addressDTO);
            SecurityHelper.ProtectFields(addressDTO, address, fields, _protector.Protect);

            address.PersonId = personId;
            address.CreatedAt = DateTime.Now;
            address.CreatedById = userId;
            address.LastUpdatedAt = DateTime.Now;
            address.LastUpdatedById = userId;
            _repositoryManager.AddressesRepository.Add(address);

            await _repositoryManager.UnitOfWork.SaveChanges();

            var addressForReturn = _mapper.Map<AddressDTO>(addressDTO);
            addressForReturn.Id = address.Id;
            addressForReturn.PersonId = personId;
            return addressForReturn;
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
            var addressDTO = new AddressDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new AddressForAddingDTO());
            SecurityHelper.UnprotectFields(address, addressDTO, fields, _protector.Unprotect);
            addressDTO.Id = address.Id;
            addressDTO.PersonId = address.PersonId;
            return addressDTO;
        }

        public async Task<IEnumerable<AddressDTO>> GetByPersonId(int personId)
        {
            var addresses = await _repositoryManager.AddressesRepository.GetByPersonId(personId);
            if (addresses == null || !addresses.Any())
            {
                return Enumerable.Empty<AddressDTO>();
            }

            var addressDTOs = new List<AddressDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new AddressForAddingDTO());
            foreach (var address in addresses)
            {
                var addressDTO = new AddressDTO();
                SecurityHelper.UnprotectFields(address, addressDTO, fields, _protector.Unprotect);
                addressDTO.Id = address.Id;
                addressDTO.PersonId = address.PersonId;
                addressDTOs.Add(addressDTO);
            }

            return addressDTOs;
        }

        public async Task<IEnumerable<AddressDTO>> GetAll()
        {
            var addresses = await _repositoryManager.AddressesRepository.GetAll();
            if (addresses == null || !addresses.Any())
            {
                return Enumerable.Empty<AddressDTO>();
            }

            var addressDTOs = new List<AddressDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new AddressForAddingDTO());
            foreach (var address in addresses)
            {
                var addressDTO = new AddressDTO();
                SecurityHelper.UnprotectFields(address, addressDTO, fields, _protector.Unprotect);
                addressDTO.Id = address.Id;
                addressDTO.PersonId = address.PersonId;
                addressDTOs.Add(addressDTO);
            }

            return addressDTOs;
        }
    }
}
