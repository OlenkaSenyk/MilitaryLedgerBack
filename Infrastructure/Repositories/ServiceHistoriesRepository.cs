using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ServiceHistoriesRepository : IChildItemRepository<ServiceHistory>
    {
        private readonly AppDbContext _context;
        public ServiceHistoriesRepository(AppDbContext context) => _context = context;

        public void Add(ServiceHistory service)
        {
            _context.ServiceHistories.Add(service);
        }

        public void Delete(ServiceHistory service)
        {
            _context.ServiceHistories.Remove(service);
        }

        public async Task<ServiceHistory> GetById(int itemId)
        {
            return await _context.ServiceHistories.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<ServiceHistory>> GetByPersonId(int personId)
        {
            return await _context.ServiceHistories.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<ServiceHistory>> GetAll()
        {
            return await _context.ServiceHistories.ToListAsync();
        }
    }
}
