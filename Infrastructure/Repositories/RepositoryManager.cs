using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IPeopleRepository> _lazyPeopleRepository;
        private readonly Lazy<IAddressesRepository> _lazyAddressesRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(AppDbContext context)
        {
            _lazyPeopleRepository = new Lazy<IPeopleRepository>(() => new PeopleRepository(context));
            _lazyAddressesRepository = new Lazy<IAddressesRepository>(() => new AddressesRepository(context));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
        }

        public IPeopleRepository PeopleRepository => _lazyPeopleRepository.Value;
        public IAddressesRepository AddressesRepository => _lazyAddressesRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
