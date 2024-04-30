using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChildItemRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int itemId);
        Task<IEnumerable<T>> GetByPersonId(int personId);
        void Add(T T);
        void Delete(T T);
    }
}
