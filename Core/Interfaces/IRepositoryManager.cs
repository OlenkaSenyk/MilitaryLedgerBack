using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Entities.File;

namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IPeopleRepository PeopleRepository { get; }
        IUsersRepository UsersRepository { get; }
        IUnitOfWork UnitOfWork { get; }
        IChildItemRepository<Address> AddressesRepository { get; }
        IChildItemRepository<Award> AwardsRepository { get; }
        IChildItemRepository<CombatParticipation> CombatParticipationsRepository { get; }
        IChildItemRepository<Injurie> InjuriesRepository { get; }
        IChildItemRepository<ServiceHistory> ServiceHistoriesRepository { get; }
        IChildItemRepository<Parameter> ParametersRepository { get; }
        IChildItemRepository<MedicalData> MedicalDatasRepository { get; }
        IChildItemRepository<File> FilesRepository { get; }
    }
}
