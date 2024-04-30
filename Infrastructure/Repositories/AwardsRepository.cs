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
    public class AwardsRepository : IChildItemRepository<Award>
    {
        private readonly AppDbContext _context;
        public AwardsRepository(AppDbContext context) => _context = context;

        public void Add(Award award)
        {
            _context.Awards.Add(award);
        }

        public void Delete(Award award)
        {
            _context.Awards.Remove(award);
        }

        public async Task<Award> GetById(int itemId)
        {
            return await _context.Awards.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<Award>> GetByPersonId(int personId)
        {
            return await _context.Awards.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<Award>> GetAll()
        {
            return await _context.Awards.ToListAsync();
        }
    }
}
