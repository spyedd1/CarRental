using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Controllers
{
    public class GymPassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymPassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GymPasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GymPass.Include(g => g.GymCustomer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GymPasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymPass = await _context.GymPass
                .Include(g => g.GymCustomer)
                .FirstOrDefaultAsync(m => m.GymPassId == id);
            if (gymPass == null)
            {
                return NotFound();
            }

            return View(gymPass);
        }

        // GET: GymPasses/Create
        public IActionResult Create()
        {
            ViewData["GymCustomerId"] = new SelectList(_context.GymCustomer, "GymCustomerId", "GymCustomerId");
            return View();
        }

        // POST: GymPasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymPassId,GymCustomerId,CustomerName,ExpiryDate")] GymPass gymPass)
        {

            var gymCustomer = await _context.GymCustomer.FindAsync(gymPass.GymCustomerId);

            if (gymCustomer == null)
            {
                return NotFound();
            }

            gymPass.GymCustomer = gymCustomer;

            ModelState.Remove("GymCustomerId");
            if (ModelState.IsValid)
            {
                _context.Add(gymPass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymCustomerId"] = new SelectList(_context.GymCustomer, "GymCustomerId", "GymCustomerId", gymPass.GymCustomerId);
            return View(gymPass);
        }

        // GET: GymPasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymPass = await _context.GymPass.FindAsync(id);
            if (gymPass == null)
            {
                return NotFound();
            }
            ViewData["GymCustomerId"] = new SelectList(_context.GymCustomer, "GymCustomerId", "GymCustomerId", gymPass.GymCustomerId);
            return View(gymPass);
        }

        // POST: GymPasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymPassId,GymCustomerId,CustomerName,ExpiryDate")] GymPass gymPass)
        {


            var gymCustomer = await _context.GymCustomer.FindAsync(gymPass.GymCustomerId);

            if (gymCustomer == null)
            {
                return NotFound();
            }

            gymPass.GymCustomer = gymCustomer;

            ModelState.Remove("GymCustomerId");

            if (id != gymPass.GymPassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymPass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymPassExists(gymPass.GymPassId))
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
            ViewData["GymCustomerId"] = new SelectList(_context.GymCustomer, "GymCustomerId", "GymCustomerId", gymPass.GymCustomerId);
            return View(gymPass);
        }

        // GET: GymPasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymPass = await _context.GymPass
                .Include(g => g.GymCustomer)
                .FirstOrDefaultAsync(m => m.GymPassId == id);
            if (gymPass == null)
            {
                return NotFound();
            }

            return View(gymPass);
        }

        // POST: GymPasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymPass = await _context.GymPass.FindAsync(id);
            if (gymPass != null)
            {
                _context.GymPass.Remove(gymPass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymPassExists(int id)
        {
            return _context.GymPass.Any(e => e.GymPassId == id);
        }
    }
}
