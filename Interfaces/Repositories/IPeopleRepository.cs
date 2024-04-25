using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IPeopleRepository : IDisposable
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
        void Create(Person person);
        void Update(Person person);
        void Delete(int id);
        void Save();
    }
}
