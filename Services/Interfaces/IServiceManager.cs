using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IPeopleService PeopleService { get; }
        ILoginService LoginService { get; }
        IChildItemService<AddressDTO, AddressForAddingDTO> AddressesService { get; }
        IChildItemService<AwardDTO, AwardForAddingDTO> AwardsService { get; }
        IChildItemService<CombatParticipationDTO, CombatParticipationForAddingDTO> CombatParticipationsService { get; }
        IChildItemService<InjurieDTO, InjurieForAddingDTO> InjuriesService { get; }
        IChildItemService<ServiceHistoryDTO, ServiceHistoryForAddingDTO> ServiceHistoriesService { get; }
        IChildItemService<ParameterDTO, ParameterForAddingDTO> ParametersService { get; }
        IChildItemService<MedicalDataDTO, MedicalDataForAddingDTO> MedicalDatasService { get; }
        IChildItemService<FileDTO, FileForAddingDTO> FilesService { get; }
        IAnalysisService AnalysisService { get; }
    }
}
