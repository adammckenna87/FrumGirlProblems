using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheChesedProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace TheChesedProject.Controllers
{
    public class ProductController : Controller
    {

        private readonly TCPDbContext _context;
        private readonly IHostingEnvironment _env;

        public ProductController(TCPDbContext context, IHostingEnvironment env)
        {

            _context = context;
            _env = env;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: ProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _context.Carts.FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }

            if (cart == null)
            {
                cart = new Cart();
                cartId = Guid.NewGuid();
                cart.CookieIdentifier = cartId;

                _context.Carts.Add(cart);
                Response.Cookies.Append("cartId", cartId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.MaxValue });

            }

            cart.LastModified = DateTime.Now;

            _context.SaveChanges();
            return Ok();
        }
    }
    }

