using AutoMapper;
using Domain.Interfaces;
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
        private readonly Lazy<IAddressesService> _lazyAddressesService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyPeopleService = new Lazy<IPeopleService>(() => new PeopleService(repositoryManager, mapper));
            _lazyAddressesService = new Lazy<IAddressesService>(() => new AddressesService(repositoryManager, mapper));
        }

        public IPeopleService PeopleService => _lazyPeopleService.Value;
        public IAddressesService AddressesService => _lazyAddressesService.Value;
    }
}
