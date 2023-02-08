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
    public class CatalogItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CatalogItem.Include(c => c.CatalogBrand).Include(c => c.CatalogType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CatalogItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CatalogItem == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItem
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View(catalogItem);
        }

        // GET: CatalogItems/Create
        public IActionResult Create()
        {
            ViewData["CatalogBrandId"] = new SelectList(_context.Set<CatalogBrand>(), "Id", "Brand");
            ViewData["CatalogTypeId"] = new SelectList(_context.Set<CatalogType>(), "Id", "Type");
            return View();
        }

        // POST: CatalogItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,PictureFileName,PictureUri,CatalogTypeId,CatalogBrandId,AvailableStock,RestockThreshold,MaxStockThreshold,OnReorder")] CatalogItem catalogItem)
        {
            if (ModelState.IsValid)
            {
                catalogItem.Id = Guid.NewGuid();
                _context.Add(catalogItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogBrandId"] = new SelectList(_context.Set<CatalogBrand>(), "Id", "Brand", catalogItem.CatalogBrandId);
            ViewData["CatalogTypeId"] = new SelectList(_context.Set<CatalogType>(), "Id", "Type", catalogItem.CatalogTypeId);
            return View(catalogItem);
        }

        // GET: CatalogItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CatalogItem == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItem.FindAsync(id);
            if (catalogItem == null)
            {
                return NotFound();
            }
            ViewData["CatalogBrandId"] = new SelectList(_context.Set<CatalogBrand>(), "Id", "Brand", catalogItem.CatalogBrandId);
            ViewData["CatalogTypeId"] = new SelectList(_context.Set<CatalogType>(), "Id", "Type", catalogItem.CatalogTypeId);
            return View(catalogItem);
        }

        // POST: CatalogItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,PictureFileName,PictureUri,CatalogTypeId,CatalogBrandId,AvailableStock,RestockThreshold,MaxStockThreshold,OnReorder")] CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogItemExists(catalogItem.Id))
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
            ViewData["CatalogBrandId"] = new SelectList(_context.Set<CatalogBrand>(), "Id", "Brand", catalogItem.CatalogBrandId);
            ViewData["CatalogTypeId"] = new SelectList(_context.Set<CatalogType>(), "Id", "Type", catalogItem.CatalogTypeId);
            return View(catalogItem);
        }

        // GET: CatalogItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CatalogItem == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItem
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View(catalogItem);
        }

        // POST: CatalogItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CatalogItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CatalogItem'  is null.");
            }
            var catalogItem = await _context.CatalogItem.FindAsync(id);
            if (catalogItem != null)
            {
                _context.CatalogItem.Remove(catalogItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogItemExists(Guid id)
        {
          return (_context.CatalogItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
