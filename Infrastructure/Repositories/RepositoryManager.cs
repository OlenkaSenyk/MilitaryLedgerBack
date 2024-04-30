using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Entities.File;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IPeopleRepository> _lazyPeopleRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
        private readonly Lazy<IUsersRepository> _lazyUsersRepository;
        private readonly Lazy<IChildItemRepository<Address>> _lazyAddressesRepository;
        private readonly Lazy<IChildItemRepository<Award>> _lazyAwardsRepository;
        private readonly Lazy<IChildItemRepository<CombatParticipation>> _lazyCombatParticipationsRepository;
        private readonly Lazy<IChildItemRepository<Injurie>> _lazyInjuriesRepository;
        private readonly Lazy<IChildItemRepository<ServiceHistory>> _lazyServiceHistoriesRepository;
        private readonly Lazy<IChildItemRepository<Parameter>> _lazyParametersRepository;
        private readonly Lazy<IChildItemRepository<MedicalData>> _lazyMedicalDatasRepository;
        private readonly Lazy<IChildItemRepository<File>> _lazyFilesRepository;

        public RepositoryManager(AppDbContext context)
        {
            _lazyPeopleRepository = new Lazy<IPeopleRepository>(() => new PeopleRepository(context));

            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
            _lazyUsersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _lazyAddressesRepository = new Lazy<IChildItemRepository<Address>>(() => new AddressesRepository(context));
            _lazyAwardsRepository = new Lazy<IChildItemRepository<Award>>(() => new AwardsRepository(context));
            _lazyCombatParticipationsRepository = new Lazy<IChildItemRepository<CombatParticipation>>(() =>
                new CombatParticipationsRepository(context));
            _lazyInjuriesRepository = new Lazy<IChildItemRepository<Injurie>>(() => new InjuriesRepository(context));
            _lazyServiceHistoriesRepository = new Lazy<IChildItemRepository<ServiceHistory>>(() =>
                new ServiceHistoriesRepository(context));
            _lazyParametersRepository = new Lazy<IChildItemRepository<Parameter>>
                (() => new ParametersRepository(context));
            _lazyMedicalDatasRepository = new Lazy<IChildItemRepository<MedicalData>>
                (() => new MedicalDatasRepository(context));
            _lazyFilesRepository = new Lazy<IChildItemRepository<File>>(() => new FilesRepository(context));

        }

        public IPeopleRepository PeopleRepository => _lazyPeopleRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
        public IUsersRepository UsersRepository => _lazyUsersRepository.Value;
        public IChildItemRepository<Address> AddressesRepository => _lazyAddressesRepository.Value;
        public IChildItemRepository<Award> AwardsRepository => _lazyAwardsRepository.Value;
        public IChildItemRepository<CombatParticipation> CombatParticipationsRepository => _lazyCombatParticipationsRepository.Value;
        public IChildItemRepository<Injurie> InjuriesRepository => _lazyInjuriesRepository.Value;
        public IChildItemRepository<ServiceHistory> ServiceHistoriesRepository => _lazyServiceHistoriesRepository.Value;
        public IChildItemRepository<Parameter> ParametersRepository => _lazyParametersRepository.Value;
        public IChildItemRepository<MedicalData> MedicalDatasRepository => _lazyMedicalDatasRepository.Value;
        public IChildItemRepository<File> FilesRepository => _lazyFilesRepository.Value;
    }
}
