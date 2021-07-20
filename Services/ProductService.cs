using CascadasPOS.Data;
using CascadasPOS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Services
{
    public class ProductService : ICrudService<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Product item)
        {
            if (item.FormFile != null)
            {
                var fileName = Utilities.SaveImage(item.FormFile);

                item.Image = fileName;
            }

            bool saved = false;

            if (item != null)
            {
                await _context.Products.AddAsync(item);
                saved = await _context.SaveChangesAsync() > 0;
            }

            return saved;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = false;
            var item = await _context.Products.FindAsync(id);

            if (item != null)
            {
                _context.Products.Remove(item);
                deleted = await _context.SaveChangesAsync() > 0;
            }

            return deleted;
        }

        public async Task<List<Product>> GetAllAsync() => await _context.Products.Include(t => t.Tax).Include(c => c.Category).ToListAsync();

        public async Task<Product> GetAsync(int id) => await _context.Products.Include(x => x.Tax).Include(x => x.Category)
            .Where(s => s.Id == id).FirstOrDefaultAsync();

        public async Task<bool> UpdateAsync(Product item)
        {
            bool updated = false;
            var product = await _context.Products.FindAsync(item.Id);

            if (product != null)
            {
                item.DateUpdated = DateTime.Now.ToLocalTime();
                _context.Entry(item).State = EntityState.Modified;
                updated = await _context.SaveChangesAsync() > 0;
            }

            return updated;
        }

        public async Task<(List<Tax> taxes, List<Category> categories)> GetRelations() =>
            (await _context.Taxes.ToListAsync(), await _context.Categories.ToListAsync());

    }
}
