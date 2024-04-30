using AutoMapper;
using Contracts.DTO;
using Contracts.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Parameter = Domain.Entities.Parameter;

namespace Services.Services
{
    public class ParametersService : IChildItemService<ParameterDTO, ParameterForAddingDTO>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ParametersService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ParameterDTO> Add(int personId, ParameterForAddingDTO parameterDTO, string token)
        {
            var person = await _repositoryManager.PeopleRepository.GetPersonById(personId);
            if (person is null)
            {
                //throw new PersonNotFoundException(personId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);

            var parameter = _mapper.Map<Parameter>(parameterDTO);
            parameter.PersonId = personId;
            parameter.CreatedAt = DateTime.Now;
            parameter.CreatedById = userId;
            parameter.LastUpdatedAt = DateTime.Now;
            parameter.LastUpdatedById = userId;
            _repositoryManager.ParametersRepository.Add(parameter);

            await _repositoryManager.UnitOfWork.SaveChanges();
            return _mapper.Map<ParameterDTO>(parameter);
        }

        public async Task Delete(int parameterId)
        {
            var parameter = await _repositoryManager.ParametersRepository.GetById(parameterId);
            if (parameter is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            _repositoryManager.ParametersRepository.Delete(parameter);
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task DeleteAllByPersonId(int personId)
        {
            var parameters = await _repositoryManager.ParametersRepository.GetByPersonId(personId);
            foreach (var a in parameters)
            {
                _repositoryManager.ParametersRepository.Delete(a);
            }
            await _repositoryManager.UnitOfWork.SaveChanges();
        }

        public async Task<ParameterDTO> GetById(int parameterId)
        {
            var parameter = await _repositoryManager.ParametersRepository.GetById(parameterId);
            if (parameter is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            var parameterDTO = _mapper.Map<ParameterDTO>(parameter);
            return parameterDTO;
        }

        public async Task<IEnumerable<ParameterDTO>> GetByPersonId(int personId)
        {
            var parameters = await _repositoryManager.ParametersRepository.GetByPersonId(personId);
            return _mapper.Map<List<ParameterDTO>>(parameters);
        }

        public async Task<IEnumerable<ParameterDTO>> GetAll()
        {
            var parameters = await _repositoryManager.ParametersRepository.GetAll();
            return _mapper.Map<List<ParameterDTO>>(parameters);
        }

        public async Task Update(int parameterId, ParameterDTO parameterDTO, string token)
        {
            var parameter = await _repositoryManager.ParametersRepository.GetById(parameterId);
            if (parameter is null)
            {
                //throw new AddressNotFoundException(addressId);
            }
            int userId = SecurityHelper.GetClaimsFromToken(token);
            parameter.Width = parameterDTO.Width;
            parameter.Height = parameterDTO.Height;
            parameter.ShoeSize = parameterDTO.ShoeSize;
            parameter.ClothingSize = parameterDTO.ClothingSize;
            parameter.GasMaskSize = parameterDTO.GasMaskSize;
            parameter.HeadCircumference = parameterDTO.HeadCircumference;
            parameter.LastUpdatedAt = DateTime.Now;
            parameter.LastUpdatedById = userId;

            await _repositoryManager.UnitOfWork.SaveChanges();
        }
    }
}
