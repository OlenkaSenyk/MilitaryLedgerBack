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

namespace Services.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PeopleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<PersonDTO> Add(PersonForAddingDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            _repositoryManager.PeopleRepository.Add(person);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<PersonDTO>(person);
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
            return _mapper.Map<List<PersonDTO>>(people);
        }

        public async Task<PersonDTO> GetPersonById(int personId)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                return null;
                //throw new PersonNotFoundException(personId);
            }
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task Update(int personId, PersonDTO personDTO)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            person.FirstName = personDTO.FirstName;
            person.LastName = personDTO.LastName;
            person.MiddleName = personDTO.MiddleName;
            person.DateOfBirth = personDTO.DateOfBirth;
            person.Phone = personDTO.Phone;
            person.Nationality = personDTO.Nationality;
            person.Sex = personDTO.Sex;
            person.MaritalStatus = personDTO.MaritalStatus;
            person.Education = personDTO.Education;
            person.Workplace = personDTO.Workplace;
            person.PublicSpecialty = personDTO.PublicSpecialty;
            person.TRSSC = personDTO.TRSSC;
            person.RegistrationDate = personDTO.RegistrationDate;
            person.DischargeDate = personDTO.DischargeDate;
            person.DischargeReason = personDTO.DischargeReason;
            person.NeedMMC = personDTO.NeedMMC;
            person.LastMMC = personDTO.LastMMC;
            person.Fine = personDTO.Fine;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
