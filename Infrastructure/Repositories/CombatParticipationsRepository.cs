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
    public class CombatParticipationsRepository : IChildItemRepository<CombatParticipation>
    {
        private readonly AppDbContext _context;
        public CombatParticipationsRepository(AppDbContext context) => _context = context;

        public void Add(CombatParticipation participation)
        {
            _context.CombatParticipations.Add(participation);
        }

        public void Delete(CombatParticipation participation)
        {
            _context.CombatParticipations.Remove(participation);
        }

        public async Task<CombatParticipation> GetById(int itemId)
        {
            return await _context.CombatParticipations.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<CombatParticipation>> GetByPersonId(int personId)
        {
            return await _context.CombatParticipations.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<CombatParticipation>> GetAll()
        {
            return await _context.CombatParticipations.ToListAsync();
        }
    }
}
