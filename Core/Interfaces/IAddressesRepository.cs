using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Address>> GetAllAddresses();
        Task<Address> GetAddressById(int addressId);
        Task<IEnumerable<Address>> GetAddressesByPersonId(int personId);
        void Add(Address address);
        void Delete(Address address);
    }
}
