using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheChesedProject.Models;

namespace TheChesedProject.Controllers
{
    public class CheckOutController : Controller
    {
        private TCPDbContext _context;
        private SignInManager<TCPUser> _signInManager;
        private BraintreeGateway _brainTreeGateway;
        private SmartyStreets.USStreetApi.Client _usStreetApiClient;
        private EmailService _emailService;

        public CheckOutController(TCPDbContext context, 
            SignInManager<TCPUser> signInManager,
            Braintree.BraintreeGateway braintreeGateway,
            EmailService emailService,
            SmartyStreets.USStreetApi.Client usStreetApiClient)
        {
            this._context = context;
            this._signInManager = signInManager;
            this._brainTreeGateway = braintreeGateway;
            this._usStreetApiClient = usStreetApiClient;
            this._emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            CheckOutViewModel model = new CheckOutViewModel();
            await GetCurrentCart(model);

            if (User.Identity.IsAuthenticated)
            {
                TCPUser currentUser = await _signInManager.UserManager.GetUserAsync(User);
                Braintree.CustomerSearchRequest search = new Braintree.CustomerSearchRequest();
                search.Email.Is(currentUser.Email);
                var searchResult = await _brainTreeGateway.Customer.SearchAsync(search);
                if (searchResult.Ids.Count > 0)
                {
                    Braintree.Customer customer = searchResult.FirstItem;
                    model.CreditCards = customer.CreditCards;
                    model.Addresses = customer.Addresses;
                }
            }
            if (model.Cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private async Task GetCurrentCart(CheckOutViewModel model)
        {
            Guid cartId;
            Cart cart = null;

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _signInManager.UserManager.GetUserAsync(User);
                model.email = currentUser.Email;
                model.phoneNumber = currentUser.PhoneNumber;
            }

            if (Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = await _context.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartitems => cartitems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);
                }
            }
            model.Cart = cart;

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(CheckOutViewModel model)
        {

            await GetCurrentCart(model);

            if (ModelState.IsValid)
            {
                Order newOrder = new Order
                {
                    TrackingNumber = Guid.NewGuid().ToString(),
                    OrderDate = DateTime.Now,
                    OrderItems = model.Cart.CartItems.Select(x => new OrderItem
                    {
                        ProductID = x.Product.ID,
                        ProductName = x.Product.Name,
                        ProductPrice = (x.Product.Price ?? 0),
                        Quantity = x.Quantity
                    }).ToArray(),
                    BillingAddress = model.BillingAddressLine1 + model.BillingAddressLine2,
                    ShippingAddress = model.ShippingAddressLine1 + model.ShippingAddressLine2,
                    State = model.ShippingState,
                    Country = model.ShippingCountry,
                    Email = model.email,
                    phoneNumber = model.phoneNumber,
                    Locale = model.ShippingLocale,
                    PostalCode = model.ShippingZipcode,
                    Region = model.ShippingRegion

                };

                
                TransactionRequest transaction = new TransactionRequest
                {
                    //Amount = 1,
                    Amount = model.Cart.CartItems.Sum(x => x.Quantity * (x.Product.Price ?? 0)),
                    CreditCard = new TransactionCreditCardRequest
                    {
                        Number = model.CardNumber,
                        CardholderName = model.NameOnCard,
                        CVV = model.CVV,
                        ExpirationMonth = model.BillingCardExpirationMonth.ToString().PadLeft(2, '0'),
                        ExpirationYear = model.BillingCardExpirationYear.ToString()
                    }
                    
                };
                var transactionResult = await _brainTreeGateway.Transaction.SaleAsync(transaction);
                _context.Orders.Add(newOrder);
                _context.CartItems.RemoveRange(model.Cart.CartItems);
                _context.Carts.Remove(model.Cart);
                await _context.SaveChangesAsync();



                //Try to checkout
                Response.Cookies.Delete("cartId");
                return RedirectToAction("Index", "Receipt", new { id = newOrder.TrackingNumber });
            }


            return View(model);
        }


        public IActionResult ValidateAddress(string addressLine1, string addressLine2, string region, string locale, string country, string postalCode)
        {
            if (country == "United States of America")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup
                {
                    City = locale,
                    State = region,
                    Street = addressLine1,
                    Street2 = addressLine2,
                    ZipCode = postalCode
                };
                _usStreetApiClient.Send(lookup);

                return Json(lookup.Result.Select(x => new
                {
                    AddressLine1 = x.DeliveryLine1,
                    AddressLine2 = x.DeliveryLine2,
                    PostalCode = x.Components.ZipCode,
                    Region = x.Components.State,
                    Locale = x.Components.CityName
                }));
            }
            else
            {
                return Json(new[]{ new {
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            PostalCode = postalCode,
            Region = region,
            Locale = locale
        }});
            }
        }
    }
}