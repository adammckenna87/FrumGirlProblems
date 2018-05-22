using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheChesedProject.Models;

namespace TheChesedProject.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly TCPDbContext _context;

        public CheckOutController(TCPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            CheckOutViewModel model = new CheckOutViewModel();

                Guid cartId;
                Order order = new Order();
                if (Request.Cookies.ContainsKey("cartId"))
                { 
                    if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                    {
                        
                        Cart cart = _context.Carts
                            .Include(carts => carts.CartItems)
                            .ThenInclude(cartitems => cartitems.Product)
                            .FirstOrDefault(x => x.CookieIdentifier == cartId);

                        for (int i = 0; i < cart.CartItems.Count; i++)
                        {
                            order.OrderItems.Add(new OrderItem
                            {
                                Product = cart.CartItems.ElementAt(i).Product,
                                Quantity = cart.CartItems.ElementAt(i).Quantity
                            });

                        }

                        _context.CartItems.RemoveRange(cart.CartItems);
                        _context.Carts.Remove(cart);
                        
                    
                            
                        

                        order.PaymentInfo = model.CardNumber + model.NameOnCard + model.ExpirationDate + model.CVV;
                        order.BillingAddress = model.BillingAddressLine1 + model.BillingAddressLine2;
                        order.ShippingAddress = model.ShippingAddressLine1 + model.ShippingAddressLine2;
                        order.firstName = model.firstName;
                        order.lastName = model.lastName;
                        order.phoneNumber = model.phoneNumber;

                    }
                }
                
            
            return View(model);
        }
    }
}