using Contracts.DTO;
using Contracts.DTO.AnalysisDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAnalysisService
    {
        Task<IEnumerable<StatisticWithNamesAndCountDTO>> GetStatisticByPersonField(IEnumerable<PersonDTO> people, Func<PersonDTO, string> getField);
        Task<IEnumerable<StatisticWithNamesAndCountDTO>> GetStatisticByServiceHistoriesField(IEnumerable<ServiceHistoryDTO> services, Func<ServiceHistoryDTO, string> getField);
    }
}
