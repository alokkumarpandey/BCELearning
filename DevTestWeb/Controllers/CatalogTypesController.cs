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
    public class CatalogTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogTypes
        public async Task<IActionResult> Index()
        {
              return _context.CatalogType != null ? 
                          View(await _context.CatalogType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CatalogType'  is null.");
        }

        // GET: CatalogTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CatalogType == null)
            {
                return NotFound();
            }

            var catalogType = await _context.CatalogType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogType == null)
            {
                return NotFound();
            }

            return View(catalogType);
        }

        // GET: CatalogTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] CatalogType catalogType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogType);
        }

        // GET: CatalogTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CatalogType == null)
            {
                return NotFound();
            }

            var catalogType = await _context.CatalogType.FindAsync(id);
            if (catalogType == null)
            {
                return NotFound();
            }
            return View(catalogType);
        }

        // POST: CatalogTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] CatalogType catalogType)
        {
            if (id != catalogType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogTypeExists(catalogType.Id))
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
            return View(catalogType);
        }

        // GET: CatalogTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CatalogType == null)
            {
                return NotFound();
            }

            var catalogType = await _context.CatalogType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogType == null)
            {
                return NotFound();
            }

            return View(catalogType);
        }

        // POST: CatalogTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CatalogType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CatalogType'  is null.");
            }
            var catalogType = await _context.CatalogType.FindAsync(id);
            if (catalogType != null)
            {
                _context.CatalogType.Remove(catalogType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogTypeExists(int id)
        {
          return (_context.CatalogType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
