using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPeopleService
    {
        Task<IEnumerable<PersonDTO>> GetAllPeople();
        Task<PersonDTO> GetPersonById(int personId);
        Task<PersonDTO> Add(PersonForAddingDTO personDTO, string token);
        Task Update(int personId, PersonDTO personDTO, string token);
        Task Delete(int personId);
    }
}
