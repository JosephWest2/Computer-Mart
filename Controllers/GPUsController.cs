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
    public class GPUsController : Controller
    {
        private readonly Computer_MartContext _context;

        public GPUsController(Computer_MartContext context)
        {
            _context = context;
        }

        // GET: GPUs
        public async Task<IActionResult> Index()
        {
              return _context.GPU != null ? 
                          View(await _context.GPU.ToListAsync()) :
                          Problem("Entity set 'Computer_MartContext.GPU'  is null.");
        }

        // GET: GPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GPU == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPU == null)
            {
                return NotFound();
            }

            return View(gPU);
        }

        // GET: GPUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,pictureUrl")] GPU gPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gPU);
        }

        // GET: GPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GPU == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU.FindAsync(id);
            if (gPU == null)
            {
                return NotFound();
            }
            return View(gPU);
        }

        // POST: GPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,pictureUrl")] GPU gPU)
        {
            if (id != gPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPUExists(gPU.Id))
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
            return View(gPU);
        }

        // GET: GPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GPU == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPU == null)
            {
                return NotFound();
            }

            return View(gPU);
        }

        // POST: GPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GPU == null)
            {
                return Problem("Entity set 'Computer_MartContext.GPU'  is null.");
            }
            var gPU = await _context.GPU.FindAsync(id);
            if (gPU != null)
            {
                _context.GPU.Remove(gPU);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPUExists(int id)
        {
          return (_context.GPU?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
