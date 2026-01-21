using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GymApp.Controllers
{
    public class GymCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: GymCustomers
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCustomerData = await _context.GymCustomer.Where(h => userId == userId).ToListAsync();

            return View(userCustomerData);
        }

        [Authorize]
        // GET: GymCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gymCustomer = await _context.GymCustomer.FirstOrDefaultAsync(h => h.GymCustomerId == id && h.UserID == userId);

            if (gymCustomer == null)
                return Unauthorized();

            return View(gymCustomer);
        }

        [Authorize]
        // GET: GymCustomers/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [Authorize]
        // POST: GymCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymCustomerId,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier); // Claims

            if (userID == null)
            {
                return NotFound();
            }

            gymCustomer.UserID = userID;

            ModelState.Remove("userID");

            if (ModelState.IsValid)
            {
                _context.Add(gymCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymCustomer);
        }

        [Authorize]
        // GET: GymCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gymCustomer = await _context.GymCustomer.FirstOrDefaultAsync(h => h.GymCustomerId == id && h.UserID == userId);

            if (gymCustomer == null)
                return Unauthorized();

            return View(gymCustomer);
        }

        [Authorize]
        // POST: GymCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymCustomerId,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier); // Claims

            if (userID == null)
            {
                return NotFound();
            }

            gymCustomer.UserID = userID;

            ModelState.Remove("userID");

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

        [Authorize]
        // GET: GymCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCustomerData = await _context.GymCustomer.Where(h => userId == userId).ToListAsync();

            return View(userCustomerData);
        }

        [Authorize]
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
