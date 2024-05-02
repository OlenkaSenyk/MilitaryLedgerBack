using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IChildItemService<DTO, AddingDTO>
    {
        Task<IEnumerable<DTO>> GetAll();
        Task<DTO> GetById(int itemId);
        Task<IEnumerable<DTO>> GetByPersonId(int personId);
        Task<DTO> Add(int personId, AddingDTO itemDTO, string token);
        //Task Update(int itemId, DTO itemDTO, string token);
        Task Delete(int itemId);
        Task DeleteAllByPersonId(int personId);
    }
}
