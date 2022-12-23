using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Computer_Mart.Data;
using Computer_Mart.Models;
using Microsoft.AspNetCore.Authorization;

namespace Computer_Mart.Controllers
{
    [Authorize(Policy = "AdminRequired")]
    public class CPUsController : Controller
    {
        private readonly Computer_MartContext _context;

        public CPUsController(Computer_MartContext context)
        {
            _context = context;
        }

        // GET: CPUs
        public async Task<IActionResult> Index()
        {
              return _context.CPU != null ? 
                          View(await _context.CPU.ToListAsync()) :
                          Problem("Entity set 'Computer_MartContext.CPU'  is null.");
        }

        // GET: CPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CPU == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // GET: CPUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,pictureUrl")] CPU cPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPU);
        }

        // GET: CPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CPU == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPU.FindAsync(id);
            if (cPU == null)
            {
                return NotFound();
            }
            return View(cPU);
        }

        // POST: CPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,pictureUrl")] CPU cPU)
        {
            if (id != cPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPUExists(cPU.Id))
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
            return View(cPU);
        }

        // GET: CPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CPU == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // POST: CPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CPU == null)
            {
                return Problem("Entity set 'Computer_MartContext.CPU'  is null.");
            }
            var cPU = await _context.CPU.FindAsync(id);
            if (cPU != null)
            {
                _context.CPU.Remove(cPU);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPUExists(int id)
        {
          return (_context.CPU?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
