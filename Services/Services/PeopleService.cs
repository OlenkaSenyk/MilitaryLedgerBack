using Contracts.DTO;
using Contracts.Helpers;
using Domain.Interfaces;
using Domain.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using System.Net;

namespace Services.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;
        private readonly string[] fields;

        public PeopleService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
            fields = new string[] { "FirstName", "LastName", "MiddleName", "DateOfBirth", "Phone", "PublicSpecialty", "Workplace", "Signature" };
        }

        public async Task<PersonDTO> Add(PersonForAddingDTO personDTO, string token)
        {
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var person = _mapper.Map<Person>(personDTO);
            var personForReturn = _mapper.Map<PersonDTO>(personDTO);
            SecurityHelper.ProtectFields(personForReturn, person, fields, _protector.Protect);
            person.CreatedAt = DateTime.Now;
            person.CreatedById = userId;
            person.LastUpdatedAt = DateTime.Now;
            person.LastUpdatedById = userId;
            _repositoryManager.PeopleRepository.Add(person);

            await _repositoryManager.UnitOfWork.SaveChanges();

            personForReturn.Id = person.Id;
            return personForReturn;
        }

        public async Task Delete(int personId)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            _repositoryManager.PeopleRepository.Delete(person);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPeople()
        {
            var people = await _repositoryManager.PeopleRepository.GetAllPeople();
            var peopleDTOs = new List<PersonDTO>();
            foreach (var person in people)
            {
                var personDTO = _mapper.Map<PersonDTO>(person);
                SecurityHelper.UnprotectFields(person, personDTO, fields, _protector.Unprotect);
                peopleDTOs.Add(personDTO);
            }
            return peopleDTOs;
        }

        public async Task<PersonDTO> GetPersonById(int personId)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                return null;
                //throw new PersonNotFoundException(personId);
            }
            var personDTO = _mapper.Map<PersonDTO>(person);
            SecurityHelper.UnprotectFields(person, personDTO, fields, _protector.Unprotect);
            return personDTO;
        }

        public async Task Update(int personId, PersonDTO personDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            person.FirstName = personDTO.FirstName;
            person.LastName = personDTO.LastName;
            person.MiddleName = personDTO.MiddleName;
            person.DateOfBirth = personDTO.DateOfBirth.ToString();
            person.Phone = personDTO.Phone;
            person.Nationality = personDTO.Nationality;
            person.Sex = personDTO.Sex;
            person.MaritalStatus = personDTO.MaritalStatus;
            person.Education = personDTO.Education;
            person.EducationLevel = personDTO.EducationLevel;
            person.Workplace = personDTO.Workplace;
            person.PublicSpecialty = personDTO.PublicSpecialty;
            person.TRSSC = personDTO.TRSSC;
            person.RegistrationDate = personDTO.RegistrationDate;
            person.DischargeDate = personDTO.DischargeDate;
            person.DischargeReason = personDTO.DischargeReason;
            person.NeedMMC = personDTO.NeedMMC;
            person.LastMMC = personDTO.LastMMC;
            person.Fine = personDTO.Fine;
            person.LastUpdatedAt = DateTime.Now;
            person.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
