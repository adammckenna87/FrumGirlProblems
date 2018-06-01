using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheChesedProject.Models;

namespace TheChesedProject.Controllers
{
    public class HomeController : Controller
    {

        TCPDbContext _db;

        public HomeController(TCPDbContext tcpDbContext)
        {
            _db = tcpDbContext;
        }



        public async Task<IActionResult> Index()
        {
            ViewData["Categories"] = await _db.Gemachs.Select(x => x.Category).Distinct().ToArrayAsync();
            ViewData["Cities"] = await _db.Gemachs.Select(x => x.City).Distinct().ToArrayAsync();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Boss()
        {
            return View();
           // return Json("You're a boss");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CartSummary()
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _db.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartitems => cartitems.Product)
                        .FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }
            if (cart == null)
            {
                cart = new Cart();
            }
            return Json(cart);
        }
    }
}
