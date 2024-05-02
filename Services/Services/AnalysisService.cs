using AutoMapper;
using Contracts.DTO;
using Contracts.DTO.AnalysisDTO;
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

namespace Services.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public AnalysisService(IRepositoryManager repositoryManager, IMapper mapper, IDataProtectionProvider provider)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _protector = provider.CreateProtector(GetType().Name);
        }

        public async Task<IEnumerable<StatisticWithNamesAndCountDTO>> GetStatisticByPersonField(IEnumerable<PersonDTO> people, Func<PersonDTO, string> getField)
        {
            return people.GroupBy(getField)
                .Select(g => new StatisticWithNamesAndCountDTO { Name = g.Key, Count = g.Count() })
                .ToList();
        }

        public async Task<IEnumerable<StatisticWithNamesAndCountDTO>> GetStatisticByServiceHistoriesField(IEnumerable<ServiceHistoryDTO> services, Func<ServiceHistoryDTO, string> getField)
        {
            return services.GroupBy(getField)
                .Select(g => new StatisticWithNamesAndCountDTO { Name = g.Key, Count = g.Count() })
                .ToList();
        }

    }
}
