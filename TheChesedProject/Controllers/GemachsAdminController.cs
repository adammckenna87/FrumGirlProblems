using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheChesedProject.Models;

namespace TheChesedProject.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class GemachsAdminController : Controller
    {
        private readonly TCPDbContext _context;
        private readonly IHostingEnvironment _env;

        public GemachsAdminController(TCPDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Gemachs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gemachs.ToListAsync());
        }

        // GET: Gemachs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemach = await _context.Gemachs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (gemach == null)
            {
                return NotFound();
            }

            return View(gemach);
        }

        // GET: Gemachs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Name,Description,City,Community,OwnerFirstName,OwnerLastName,PhoneNumber")] Gemach gemach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gemach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gemach);
        }

        // GET: Gemachs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemach = await _context.Gemachs.SingleOrDefaultAsync(m => m.ID == id);
            if (gemach == null)
            {
                return NotFound();
            }
            return View(gemach);
        }

        // POST: Gemachs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Name,Description,City,Community,OwnerFirstName,OwnerLastName,PhoneNumber")] Gemach gemach)
        {
            if (id != gemach.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gemach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GemachExists(gemach.ID))
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
            return View(gemach);
        }

        // GET: Gemachs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemach = await _context.Gemachs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (gemach == null)
            {
                return NotFound();
            }

            return View(gemach);
        }

        // POST: Gemachs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gemach = await _context.Gemachs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Gemachs.Remove(gemach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GemachExists(int id)
        {
            return _context.Gemachs.Any(e => e.ID == id);
        }
    }
}
