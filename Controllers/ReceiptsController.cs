using CascadasPOS.Data;
using CascadasPOS.Models;
using CascadasPOS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<JsonResult> SearchProduct(string search, int qty = 1)
        {
            var products = await _context.Products.Where(x => x.Name.Contains(search)).Select(x => new
            {
                value = new
                {
                    x.Id,
                    x.SalesPrice,
                    taxPrice = (_context.Taxes.Where(t => t.Id == x.TaxId).Select(s => s.Percentage).FirstOrDefault() / 100) * x.SalesPrice
                    quantity = qty
                },
                text = x.Name
            }).ToListAsync();

            return Json(products);
        }
    }
}
