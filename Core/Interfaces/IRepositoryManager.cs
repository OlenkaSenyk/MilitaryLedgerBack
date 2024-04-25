using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IAddressesRepository AddressesRepository { get; }
        IPeopleRepository PeopleRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
