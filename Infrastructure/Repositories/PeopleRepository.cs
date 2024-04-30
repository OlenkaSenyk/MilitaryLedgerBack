using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _context;
        public PeopleRepository(AppDbContext context) => _context = context;

        public void Add(Person person)
        {
            _context.People.Add(person);
        }

        public void Delete(Person person)
        {
            _context.People.Remove(person);
        }

        public async Task<Person> GetPersonById(int personId)
        {
            return await _context.People
                .Include(x => x.Addresses)
                .Include(x => x.Awards)
                .Include(x => x.CombatParticipations)
                .Include(x => x.Documents)
                .Include(x => x.Files)
                .Include(x => x.Injuries)
                .Include(x => x.MedicalData)
                .Include(x => x.Parameter)
                .Include(x => x.ServiceHistories)
                .FirstOrDefaultAsync(x => x.Id == personId);
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            return await _context.People
                .Include(x => x.Addresses)
                .Include(x => x.Awards)
                .Include(x => x.CombatParticipations)
                .Include(x => x.Documents)
                .Include(x => x.Files)
                .Include(x => x.Injuries)
                .Include(x => x.MedicalData)
                .Include(x => x.Parameter)
                .Include(x => x.ServiceHistories)
                .ToListAsync();
        }
    }
}
