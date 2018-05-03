using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrumGirlProblems.Models;

namespace FrumGirlProblems.Controllers
{
    public class ProductController : Controller
    {

        private List<Product> _products;

        public ProductController() => _products = new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "Boss-Melech",
                    Description = "Boss Melech T-Shirt",
                    Image = "/images/clothes.jpg",
                    Price = 29.99m,
                    Sizes = new string[] {"Small", "Medium", "Large" }
                },

                new Product
                {
                    ID = 2,
                    Name = "FGP",
                    Description = "FGP T-Shirt",
                    Image = "/images/tshirt.jpg",
                    Price = 29.99m,
                    Sizes = new string[] {"Small", "Medium", "Large" }
                }

            };

        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Product p = _products.Single(x => x.ID == id.Value);
                return View(p);
            }
            return NotFound();
        }

        public IActionResult Index()
        { 
            return View(_products);
        }
    }
}