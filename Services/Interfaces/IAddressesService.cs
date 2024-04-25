using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAddressesService
    {
        Task<IEnumerable<AddressDTO>> GetAllAddresses();
        Task<AddressDTO> GetAddressById(int addressId);
        Task<IEnumerable<AddressDTO>> GetAddressesByPersonId(int personId);
        Task<AddressDTO> Add(int personId, AddressForAddingDTO addressDTO);
        Task Update(int addressId, AddressDTO addressDTO);
        Task Delete(int addressId);
        Task DeleteAllByPersonId(int personId);
    }
}
