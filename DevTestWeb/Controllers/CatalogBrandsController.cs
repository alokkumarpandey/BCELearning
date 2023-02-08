using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommonEnitity.Catalog;
using DevTestWeb.Data;

namespace DevTestWeb.Controllers
{
    public class CatalogBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogBrands
        public async Task<IActionResult> Index()
        {
              return _context.CatalogBrand != null ? 
                          View(await _context.CatalogBrand.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CatalogBrand'  is null.");
        }

        // GET: CatalogBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CatalogBrand == null)
            {
                return NotFound();
            }

            var catalogBrand = await _context.CatalogBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogBrand == null)
            {
                return NotFound();
            }

            return View(catalogBrand);
        }

        // GET: CatalogBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand")] CatalogBrand catalogBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogBrand);
        }

        // GET: CatalogBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CatalogBrand == null)
            {
                return NotFound();
            }

            var catalogBrand = await _context.CatalogBrand.FindAsync(id);
            if (catalogBrand == null)
            {
                return NotFound();
            }
            return View(catalogBrand);
        }

        // POST: CatalogBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand")] CatalogBrand catalogBrand)
        {
            if (id != catalogBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogBrandExists(catalogBrand.Id))
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
            return View(catalogBrand);
        }

        // GET: CatalogBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CatalogBrand == null)
            {
                return NotFound();
            }

            var catalogBrand = await _context.CatalogBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogBrand == null)
            {
                return NotFound();
            }

            return View(catalogBrand);
        }

        // POST: CatalogBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CatalogBrand == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CatalogBrand'  is null.");
            }
            var catalogBrand = await _context.CatalogBrand.FindAsync(id);
            if (catalogBrand != null)
            {
                _context.CatalogBrand.Remove(catalogBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogBrandExists(int id)
        {
          return (_context.CatalogBrand?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
