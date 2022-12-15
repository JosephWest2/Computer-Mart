using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Computer_Mart.Data;
using Computer_Mart.Models;

namespace Computer_Mart.Controllers
{
    public class SSDsController : Controller
    {
        private readonly Computer_MartContext _context;

        public SSDsController(Computer_MartContext context)
        {
            _context = context;
        }

        // GET: SSDs
        public async Task<IActionResult> Index()
        {
              return _context.SSD != null ? 
                          View(await _context.SSD.ToListAsync()) :
                          Problem("Entity set 'Computer_MartContext.SSD'  is null.");
        }

        // GET: SSDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SSD == null)
            {
                return NotFound();
            }

            var sSD = await _context.SSD
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sSD == null)
            {
                return NotFound();
            }

            return View(sSD);
        }

        // GET: SSDs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SSDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,pictureUrl")] SSD sSD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sSD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sSD);
        }

        // GET: SSDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SSD == null)
            {
                return NotFound();
            }

            var sSD = await _context.SSD.FindAsync(id);
            if (sSD == null)
            {
                return NotFound();
            }
            return View(sSD);
        }

        // POST: SSDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,pictureUrl")] SSD sSD)
        {
            if (id != sSD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sSD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SSDExists(sSD.Id))
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
            return View(sSD);
        }

        // GET: SSDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SSD == null)
            {
                return NotFound();
            }

            var sSD = await _context.SSD
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sSD == null)
            {
                return NotFound();
            }

            return View(sSD);
        }

        // POST: SSDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SSD == null)
            {
                return Problem("Entity set 'Computer_MartContext.SSD'  is null.");
            }
            var sSD = await _context.SSD.FindAsync(id);
            if (sSD != null)
            {
                _context.SSD.Remove(sSD);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SSDExists(int id)
        {
          return (_context.SSD?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
