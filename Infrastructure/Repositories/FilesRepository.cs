using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Entities.File;

namespace Infrastructure.Repositories
{
    public class FilesRepository : IChildItemRepository<File>
    {
        private readonly AppDbContext _context;
        public FilesRepository(AppDbContext context) => _context = context;

        public void Add(File file)
        {
            _context.Files.Add(file);
        }

        public void Delete(File file)
        {
            _context.Files.Remove(file);
        }

        public async Task<File> GetById(int itemId)
        {
            return await _context.Files.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<File>> GetByPersonId(int personId)
        {
            return await _context.Files.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<File>> GetAll()
        {
            return await _context.Files.ToListAsync();
        }
    }
}
