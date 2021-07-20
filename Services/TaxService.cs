using CascadasPOS.Data;
using CascadasPOS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Services
{
    public class TaxService : ICrudService<Tax>
    {
        private readonly ApplicationDbContext _context;

        public TaxService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Tax item)
        {
            bool saved = false;

            if (item != null)
            {
                await _context.Taxes.AddAsync(item);
                saved = await _context.SaveChangesAsync() > 0;
            }

            return saved;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = false;
            var item = await _context.Taxes.FindAsync(id);

            if (item != null)
            {
                _context.Taxes.Remove(item);
                deleted = await _context.SaveChangesAsync() > 0;
            }

            return deleted;
        }

        public async Task<List<Tax>> GetAllAsync() => await _context.Taxes.ToListAsync();

        public async Task<Tax> GetAsync(int id) => await _context.Taxes.FindAsync(id);

        public async Task<bool> UpdateAsync(Tax item)
        {
            bool updated = false;
            var foundItem = await _context.Taxes.FindAsync(item.Id);

            if (foundItem != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                updated = await _context.SaveChangesAsync() > 0;
            }

            return updated;
        }
    }
}
