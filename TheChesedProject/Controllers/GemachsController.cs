using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheChesedProject.Models;

namespace TheChesedProject.Controllers
{
    public class GemachController : Controller
    {

        private readonly TCPDbContext _context;
        private SignInManager<TCPUser> _signInManager;

        public GemachController(TCPDbContext context, SignInManager<TCPUser> signInManager)
        {

            _context = context;
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Index(string category, string city)
        {
            IQueryable<Gemach> gemachs = _context.Gemachs;
            if (!string.IsNullOrEmpty(category))
            {
                gemachs = gemachs.Where(x => x.Category == category);
            }
            if (!string.IsNullOrEmpty(city))
            {
                gemachs = gemachs.Where(x => x.City == city);
            }
          
            return View(await gemachs.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> AddListing()
        {
          
            return View();
        }

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
    }
}