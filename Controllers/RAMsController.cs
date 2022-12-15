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
    public class RAMsController : Controller
    {
        private readonly Computer_MartContext _context;

        public RAMsController(Computer_MartContext context)
        {
            _context = context;
        }

        // GET: RAMs
        public async Task<IActionResult> Index()
        {
              return _context.RAM != null ? 
                          View(await _context.RAM.ToListAsync()) :
                          Problem("Entity set 'Computer_MartContext.RAM'  is null.");
        }

        // GET: RAMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RAM == null)
            {
                return NotFound();
            }

            var rAM = await _context.RAM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rAM == null)
            {
                return NotFound();
            }

            return View(rAM);
        }

        // GET: RAMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,pictureUrl")] RAM rAM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rAM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rAM);
        }

        // GET: RAMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RAM == null)
            {
                return NotFound();
            }

            var rAM = await _context.RAM.FindAsync(id);
            if (rAM == null)
            {
                return NotFound();
            }
            return View(rAM);
        }

        // POST: RAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,pictureUrl")] RAM rAM)
        {
            if (id != rAM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rAM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RAMExists(rAM.Id))
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
            return View(rAM);
        }

        // GET: RAMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RAM == null)
            {
                return NotFound();
            }

            var rAM = await _context.RAM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rAM == null)
            {
                return NotFound();
            }

            return View(rAM);
        }

        // POST: RAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RAM == null)
            {
                return Problem("Entity set 'Computer_MartContext.RAM'  is null.");
            }
            var rAM = await _context.RAM.FindAsync(id);
            if (rAM != null)
            {
                _context.RAM.Remove(rAM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RAMExists(int id)
        {
          return (_context.RAM?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
