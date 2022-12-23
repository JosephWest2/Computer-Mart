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
    public class ComputersController : Controller
    {
        private readonly Computer_MartContext _context;

        public ComputersController(Computer_MartContext context)
        {
            _context = context;
        }

        // GET: Computers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var computer_MartContext = _context.Computer.Include(c => c.CPU).Include(c => c.GPU).Include(c => c.RAM).Include(c => c.SSD);
            return View(await computer_MartContext.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .Include(c => c.CPU)
                .Include(c => c.GPU)
                .Include(c => c.RAM)
                .Include(c => c.SSD)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            ViewData["CPUId"] = new SelectList(_context.CPU, "Id", "Name");
            ViewData["GPUId"] = new SelectList(_context.GPU, "Id", "Name");
            ViewData["RAMId"] = new SelectList(_context.RAM, "Id", "Name");
            ViewData["SSDId"] = new SelectList(_context.SSD, "Id", "Name");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CPUId,GPUId,RAMId,SSDId,Id,Name,Description,Price,pictureUrl")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CPUId"] = new SelectList(_context.CPU, "Id", "Name", computer.CPUId);
            ViewData["GPUId"] = new SelectList(_context.GPU, "Id", "Name", computer.GPUId);
            ViewData["RAMId"] = new SelectList(_context.RAM, "Id", "Name", computer.RAMId);
            ViewData["SSDId"] = new SelectList(_context.SSD, "Id", "Name", computer.SSDId);
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            ViewData["CPUId"] = new SelectList(_context.CPU, "Id", "Name", computer.CPUId);
            ViewData["GPUId"] = new SelectList(_context.GPU, "Id", "Name", computer.GPUId);
            ViewData["RAMId"] = new SelectList(_context.RAM, "Id", "Name", computer.RAMId);
            ViewData["SSDId"] = new SelectList(_context.SSD, "Id", "Name", computer.SSDId);
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CPUId,GPUId,RAMId,SSDId,Id,Name,Description,Price,pictureUrl")] Computer computer)
        {
            if (id != computer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.Id))
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
            ViewData["CPUId"] = new SelectList(_context.CPU, "Id", "Name", computer.CPUId);
            ViewData["GPUId"] = new SelectList(_context.GPU, "Id", "Name", computer.GPUId);
            ViewData["RAMId"] = new SelectList(_context.RAM, "Id", "Name", computer.RAMId);
            ViewData["SSDId"] = new SelectList(_context.SSD, "Id", "Name", computer.SSDId);
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .Include(c => c.CPU)
                .Include(c => c.GPU)
                .Include(c => c.RAM)
                .Include(c => c.SSD)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Computer == null)
            {
                return Problem("Entity set 'Computer_MartContext.Computer'  is null.");
            }
            var computer = await _context.Computer.FindAsync(id);
            if (computer != null)
            {
                _context.Computer.Remove(computer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(int id)
        {
          return (_context.Computer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
