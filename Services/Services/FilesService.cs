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
using File = Domain.Entities.File;

namespace Services.Services
{
    public class FilesService : IChildItemService<FileDTO, FileForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public FilesService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<FileDTO> Add(int personId, FileForAddingDTO fileDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var tempFile = _mapper.Map<FileDTO>(fileDTO);
            var file = new File();
            string[] fields = SecurityHelper.GetAllFieldsNames(fileDTO);
            SecurityHelper.ProtectFields(tempFile, file, fields, _protector.Protect);

            file.PersonId = personId;
            file.CreatedAt = DateTime.Now;
            file.CreatedById = userId;
            file.LastUpdatedAt = DateTime.Now;
            file.LastUpdatedById = userId;
            _repositoryManager.FilesRepository.Add(file);

            await _repositoryManager.UnitOfWork.SaveChanges();

            var fileForReturn = _mapper.Map<FileDTO, FileDTO>(tempFile);
            fileForReturn.Id = file.Id;
            fileForReturn.PersonId = personId;
            return fileForReturn;
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
            var fileDTO = new FileDTO();
            string[] fields = SecurityHelper.GetAllFieldsNames(new FileForAddingDTO());
            SecurityHelper.UnprotectFields(file, fileDTO, fields, _protector.Unprotect);
            fileDTO.Id = file.Id;
            fileDTO.PersonId = file.PersonId;
            return fileDTO;
        }

        public async Task<IEnumerable<FileDTO>> GetByPersonId(int personId)
        {
            var files = await _repositoryManager.FilesRepository.GetByPersonId(personId);
            if (files == null || !files.Any())
            {
                return Enumerable.Empty<FileDTO>();
            }

            var fileDTOs = new List<FileDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new FileForAddingDTO());
            foreach (var file in files)
            {
                var fileDTO = new FileDTO();
                SecurityHelper.UnprotectFields(file, fileDTO, fields, _protector.Unprotect);
                fileDTO.Id = file.Id;
                fileDTO.PersonId = file.PersonId;
                fileDTOs.Add(fileDTO);
            }

            return fileDTOs;
        }

        public async Task<IEnumerable<FileDTO>> GetAll()
        {
            var files = await _repositoryManager.FilesRepository.GetAll();
            if (files == null || !files.Any())
            {
                return Enumerable.Empty<FileDTO>();
            }

            var fileDTOs = new List<FileDTO>();
            string[] fields = SecurityHelper.GetAllFieldsNames(new FileForAddingDTO());
            foreach (var file in files)
            {
                var fileDTO = new FileDTO();
                SecurityHelper.UnprotectFields(file, fileDTO, fields, _protector.Unprotect);
                fileDTO.Id = file.Id;
                fileDTO.PersonId = file.PersonId;
                fileDTOs.Add(fileDTO);
            }

            return fileDTOs;
        }
    }
}
