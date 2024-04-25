using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> GetAllPeople();
        Task<Person> GetPersonById(int personId);
        void Add(Person person);
        void Delete(Person person);
    }
}
