using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CascadasPOS.Services;
using CascadasPOS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CascadasPOS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        // GET: Products
        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Constants.Permissions.Product.Create)]
        public async Task<IActionResult> Create()
        {
            await SetViewBags();

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code", "Name", "Description", "Barcode", "Cost", "SalesPrice", "FormFile", "PriceInclideTax", "UnitOfMeasure", "TaxId", "CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            await SetViewBags(product.CategoryId, product.TaxId);

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Constants.Permissions.Product.Edit)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            await SetViewBags(product.CategoryId, product.TaxId);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Code", "Name", "Description", "Barcode", "Cost", "SalesPrice", "FormFile", "PriceInclideTax", "UnitOfMeasure", "TaxId", "CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductExists(product.Id) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await SetViewBags(product.CategoryId, product.TaxId);

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            return Json(deleted);
        }

        private async Task<bool> ProductExists(int id)
        {
            return (await _service.GetAllAsync()).Any(p => p.Id == id);
        }

        private async Task SetViewBags(int? categoryId = null, int? taxId = null)
        {
            var (taxes, categories) = await _service.GetRelations();

            ViewData["CategoryId"] = new SelectList(categories, nameof(Category.Id), nameof(Category.Name), categoryId);
            ViewData["TaxId"] = new SelectList(taxes, nameof(Tax.Id), nameof(Tax.Name), taxId);
        }

        public async Task<IActionResult> FillDetailModal(int id)
        {
            var product = await _service.GetAsync(id);

            return PartialView("_productDetailModal", product);
        }

    }
}
