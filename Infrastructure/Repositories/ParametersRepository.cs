using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Parameter = Domain.Entities.Parameter;

namespace Infrastructure.Repositories
{
    public class ParametersRepository : IChildItemRepository<Parameter>
    {
        private readonly AppDbContext _context;
        public ParametersRepository(AppDbContext context) => _context = context;

        public void Add(Parameter parameter)
        {
            _context.Parameters.Add(parameter);
        }

        public void Delete(Parameter parameter)
        {
            _context.Parameters.Remove(parameter);
        }

        public async Task<Parameter> GetById(int itemId)
        {
            return await _context.Parameters.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<Parameter>> GetByPersonId(int personId)
        {
            return await _context.Parameters.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<Parameter>> GetAll()
        {
            return await _context.Parameters.ToListAsync();
        }
    }
}
