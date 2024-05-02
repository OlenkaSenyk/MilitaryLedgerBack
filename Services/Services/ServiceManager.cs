using AutoMapper;
using Contracts.DTO;
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
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPeopleService> _lazyPeopleService;
        private readonly Lazy<ILoginService> _lazyLoginService;
        private readonly Lazy<IChildItemService<AwardDTO, AwardForAddingDTO>> _lazyAwardsService;
        private readonly Lazy<IChildItemService<AddressDTO, AddressForAddingDTO>> _lazyAddressesService;
        private readonly Lazy<IChildItemService<CombatParticipationDTO, CombatParticipationForAddingDTO>> 
            _lazyCombatParticipationsService;
        private readonly Lazy<IChildItemService<InjurieDTO, InjurieForAddingDTO>> _lazyInjuriesService;
        private readonly Lazy<IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO>> 
            _lazyServiceHistoriesService;
        private readonly Lazy<IChildItemService<ParameterDTO, ParameterForAddingDTO>> _lazyParametersService;
        private readonly Lazy<IChildItemService<FileDTO, FileForAddingDTO>> _lazyFilesService;
        private readonly Lazy<IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO>> _lazyMedicalDatasService;
        private readonly Lazy<IAnalysisService> _lazyAnalysisService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider, string key)
        {
            _lazyPeopleService = new Lazy<IPeopleService>(() => new PeopleService(repositoryManager, mapper, provider));
            _lazyLoginService = new Lazy<ILoginService>(() => new LoginService(repositoryManager, mapper, key, provider));
            _lazyAddressesService = new Lazy<IChildItemService<AddressDTO, AddressForAddingDTO>>
                (() => new AddressesService(repositoryManager, mapper, provider));
            _lazyAwardsService = new Lazy<IChildItemService<AwardDTO, AwardForAddingDTO>>
                (() => new AwardsService(repositoryManager, mapper));
            _lazyInjuriesService = new Lazy<IChildItemService<InjurieDTO, InjurieForAddingDTO>>
                (() => new InjuriesService(repositoryManager, mapper, provider));
            _lazyServiceHistoriesService = new Lazy<IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO>>
                (() => new ServiceHistoriesService(repositoryManager, mapper, provider));
            _lazyCombatParticipationsService = new Lazy<IChildItemService<CombatParticipationDTO, 
                CombatParticipationForAddingDTO>>(() => new CombatParticipationsService(repositoryManager, mapper, provider));
            _lazyParametersService = new Lazy<IChildItemService<ParameterDTO, ParameterForAddingDTO>>
                (() => new ParametersService(repositoryManager, mapper));
            _lazyMedicalDatasService = new Lazy<IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO>>
                (() => new MedicalDatasService(repositoryManager, mapper, provider));
            _lazyFilesService = new Lazy<IChildItemService<FileDTO, FileForAddingDTO>>
                (() => new FilesService(repositoryManager, mapper, provider));
            _lazyAnalysisService = new Lazy<IAnalysisService>
                (() => new AnalysisService(repositoryManager, mapper, provider));
        }

        public IPeopleService PeopleService => _lazyPeopleService.Value;
        public ILoginService LoginService => _lazyLoginService.Value;
        public IChildItemService<AddressDTO, AddressForAddingDTO> AddressesService => _lazyAddressesService.Value;
        public IChildItemService<AwardDTO, AwardForAddingDTO> AwardsService => _lazyAwardsService.Value;
        public IChildItemService<CombatParticipationDTO, CombatParticipationForAddingDTO> 
            CombatParticipationsService => _lazyCombatParticipationsService.Value;
        public IChildItemService<InjurieDTO, InjurieForAddingDTO> InjuriesService => _lazyInjuriesService.Value;
        public IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO> 
            ServiceHistoriesService => _lazyServiceHistoriesService.Value;
        public IChildItemService<ParameterDTO, ParameterForAddingDTO> ParametersService => _lazyParametersService.Value;
        public IChildItemService<FileDTO, FileForAddingDTO> FilesService => _lazyFilesService.Value;
        public IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO> 
            MedicalDatasService => _lazyMedicalDatasService.Value;
        public IAnalysisService AnalysisService => _lazyAnalysisService.Value;
    }
}
