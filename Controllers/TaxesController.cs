using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CascadasPOS.Services;
using CascadasPOS.Models;

namespace CascadasPOS.Controllers
{
    public class TaxesController : Controller
    {
        private readonly TaxService _taxService;

        public TaxesController(TaxService taxService)
        {
            _taxService = taxService;
        }

        // GET: Taxes
        public async Task<IActionResult> Index()
        {
            return View(await _taxService.GetAllAsync());
        }

        // GET: Taxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tax = await _taxService.GetAsync(id.Value);
            if (tax == null)
            {
                return NotFound();
            }

            return View(tax);
        }

        // GET: Taxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Percentage")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                await _taxService.AddAsync(tax);
                return RedirectToAction(nameof(Index));
            }
            return View(tax);
        }

        // GET: Taxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tax = await _taxService.GetAsync(id.Value);
            if (tax == null)
            {
                return NotFound();
            }
            return View(tax);
        }

        // POST: Taxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Percentage")] Tax tax)
        {
            if (id != tax.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _taxService.UpdateAsync(tax);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ((await TaxExists(tax.Id)) == false)
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
            return View(tax);
        }

        // GET: Taxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tax = await _taxService.GetAsync(id.Value);
            if (tax == null)
            {
                return NotFound();
            }

            return View(tax);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _taxService.DeleteAsync(id);

            return Json(deleted);
        }

        public async Task<IActionResult> FillDetailModal(int id)
        {
            var tax = await _taxService.GetAsync(id);

            return PartialView("_taxDetailModal", tax);
        }

        private async Task<bool> TaxExists(int id)
        {
            return (await _taxService.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
