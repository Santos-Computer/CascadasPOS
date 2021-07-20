using CascadasPOS.Data;
using CascadasPOS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Services
{
    public class CategoryService : ICrudService<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Category item)
        {
            bool saved = false;

            if (item != null)
            {
                await _context.Categories.AddAsync(item);
                saved = await _context.SaveChangesAsync() > 0;
            }

            return saved;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = false;
            var item = await _context.Categories.FindAsync(id);

            if (item != null)
            {
                _context.Categories.Remove(item);
                deleted = await _context.SaveChangesAsync() > 0;
            }

            return deleted;
        }

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetAsync(int id) => await _context.Categories.FindAsync(id);

        public async Task<bool> UpdateAsync(Category item)
        {
            bool updated = false;
            var foundItem = await _context.Categories.FindAsync(item.Id);

            if (foundItem != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                updated = await _context.SaveChangesAsync() > 0;
            }

            return updated;
        }
    }
}
