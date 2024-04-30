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
    public class MedicalDatasRepository : IChildItemRepository<MedicalData>
    {
        private readonly AppDbContext _context;
        public MedicalDatasRepository(AppDbContext context) => _context = context;

        public void Add(MedicalData data)
        {
            _context.MedicalDatas.Add(data);
        }

        public void Delete(MedicalData data)
        {
            _context.MedicalDatas.Remove(data);
        }

        public async Task<MedicalData> GetById(int itemId)
        {
            return await _context.MedicalDatas.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<MedicalData>> GetByPersonId(int personId)
        {
            return await _context.MedicalDatas.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<MedicalData>> GetAll()
        {
            return await _context.MedicalDatas.ToListAsync();
        }
    }
}
