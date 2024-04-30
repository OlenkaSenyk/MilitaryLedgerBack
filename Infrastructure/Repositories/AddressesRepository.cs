using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AddressesRepository : IChildItemRepository<Address>
    {
        private readonly AppDbContext _context;
        public AddressesRepository(AppDbContext context) => _context = context;

        public void Add(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void Delete(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public async Task<Address> GetById(int itemId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<Address>> GetByPersonId(int personId)
        {
            return await _context.Addresses.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }
    }
}
