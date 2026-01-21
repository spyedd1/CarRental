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
    public class GymCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GymCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.GymCustomer.ToListAsync());
        }

        // GET: GymCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymCustomer = await _context.GymCustomer
                .FirstOrDefaultAsync(m => m.GymCustomerId == id);
            if (gymCustomer == null)
            {
                return NotFound();
            }

            return View(gymCustomer);
        }

        // GET: GymCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymCustomerId,UserID,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymCustomer);
        }

        // GET: GymCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymCustomer = await _context.GymCustomer.FindAsync(id);
            if (gymCustomer == null)
            {
                return NotFound();
            }
            return View(gymCustomer);
        }

        // POST: GymCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymCustomerId,UserID,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {
            if (id != gymCustomer.GymCustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymCustomerExists(gymCustomer.GymCustomerId))
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
            return View(gymCustomer);
        }

        // GET: GymCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymCustomer = await _context.GymCustomer
                .FirstOrDefaultAsync(m => m.GymCustomerId == id);
            if (gymCustomer == null)
            {
                return NotFound();
            }

            return View(gymCustomer);
        }

        // POST: GymCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymCustomer = await _context.GymCustomer.FindAsync(id);
            if (gymCustomer != null)
            {
                _context.GymCustomer.Remove(gymCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymCustomerExists(int id)
        {
            return _context.GymCustomer.Any(e => e.GymCustomerId == id);
        }
    }
}
