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
using File = Domain.Entities.File;

namespace Services.Services
{
    public class FilesService : IChildItemService<FileDTO, FileForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public FilesService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<FileDTO> Add(int personId, FileForAddingDTO fileDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var file = _mapper.Map<File>(fileDTO);
            file.PersonId = personId;
            file.CreatedAt = DateTime.Now;
            file.CreatedById = userId;
            file.LastUpdatedAt = DateTime.Now;
            file.LastUpdatedById = userId;
            _repositoryManager.FilesRepository.Add(file);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<FileDTO>(file);
        }

        public async Task Delete(int fileId)
        {
            var file = await _repositoryManager.FilesRepository.GetById(fileId);
            if (file is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.FilesRepository.Delete(file);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var files = await _repositoryManager.FilesRepository.GetByPersonId(personId);
            foreach (var a in files)
            {
                _repositoryManager.FilesRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<FileDTO> GetById(int fileId)
        {
            var file = await _repositoryManager.FilesRepository.GetById(fileId);
            if (file is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var fileDTO = _mapper.Map<FileDTO>(file);
            return fileDTO;
        }

        public async Task<IEnumerable<FileDTO>> GetByPersonId(int personId)
        {
            var files = await _repositoryManager.FilesRepository.GetByPersonId(personId);
            return _mapper.Map<List<FileDTO>>(files);
        }

        public async Task<IEnumerable<FileDTO>> GetAll()
        {
            var file = await _repositoryManager.FilesRepository.GetAll();
            return _mapper.Map<List<FileDTO>>(file);
        }

        public async Task Update(int fileId, FileDTO fileDTO, string token)
        {
            var file = await _repositoryManager.FilesRepository.GetById(fileId);
            if (file is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            file.Photo = fileDTO.Photo;
            file.Passport = fileDTO.Passport;
            file.IndividualTaxNumber = fileDTO.IndividualTaxNumber;
            file.ResidencePermit = fileDTO.ResidencePermit;
            file.MedicalCertificate = fileDTO.MedicalCertificate;
            file.LastUpdatedAt = DateTime.Now;
            file.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
