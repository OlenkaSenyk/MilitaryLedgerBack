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
    public class InjuriesRepository : IChildItemRepository<Injurie>
    {
        private readonly AppDbContext _context;
        public InjuriesRepository(AppDbContext context) => _context = context;

        public void Add(Injurie injurie)
        {
            _context.Injuries.Add(injurie);
        }

        public void Delete(Injurie injurie)
        {
            _context.Injuries.Remove(injurie);
        }

        public async Task<Injurie> GetById(int itemId)
        {
            return await _context.Injuries.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<Injurie>> GetByPersonId(int personId)
        {
            return await _context.Injuries.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<Injurie>> GetAll()
        {
            return await _context.Injuries.ToListAsync();
        }
    }
}
