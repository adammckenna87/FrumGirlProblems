using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheChesedProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using Microsoft.AspNetCore.Http.Extensions;

namespace TheChesedProject.Controllers
{
    public class AccountController : Controller
    {
        EmailService _emailService;
        Braintree.BraintreeGateway _braintreeGateway;


        SignInManager<TCPUser> _signInManager;
        public AccountController(SignInManager<TCPUser> signInManager, EmailService emailService)
        { 
            this._signInManager = signInManager;
            this._emailService = emailService;

        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Your Account.";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(Models.RegisterViewModel model)
        {
            
        
            if (ModelState.IsValid)
            {
                TCPUser newUser = new TCPUser {
                    Email = model.email,
                    UserName = model.userName,
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    PhoneNumber = model.phoneNumber
                };

                IdentityResult creationResult = this._signInManager.UserManager.CreateAsync(newUser).Result;
                if (creationResult.Succeeded)
                {

                    IdentityResult passwordResult = this._signInManager.UserManager.AddPasswordAsync(newUser, model.password).Result;
                    if (passwordResult.Succeeded)
                    {
                        Braintree.CustomerSearchRequest search = new Braintree.CustomerSearchRequest();
                        search.Email.Is(model.email);
                        var searchResult = await _braintreeGateway.Customer.SearchAsync(search);
                        if (searchResult.Ids.Count == 0)
                        {
                            //Create  a new Braintree Customer
                            await _braintreeGateway.Customer.CreateAsync(new Braintree.CustomerRequest
                            {
                                Email = model.email,
                                FirstName = model.firstName,
                                LastName = model.lastName,
                                Phone = model.phoneNumber
                            });
                        }
                        else
                        {

                            //Update the existing Braintree customer
                            Braintree.Customer existingCustomer = searchResult.FirstItem;
                            await _braintreeGateway.Customer.UpdateAsync(existingCustomer.Id, new Braintree.CustomerRequest
                            {
                                FirstName = model.firstName,
                                LastName = model.lastName,
                                Phone = model.phoneNumber
                            });
                        }

                        var confirmationToken = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(newUser);

                        confirmationToken = System.Net.WebUtility.UrlEncode(confirmationToken);

                        string currentUrl = Request.GetDisplayUrl();    //This will get me the URL for the current request
                        System.Uri uri = new Uri(currentUrl);   //This will wrap it in a "URI" object so I can split it into parts
                        string confirmationUrl = uri.GetLeftPart(UriPartial.Authority); //This gives me just the scheme + authority of the URI
                        confirmationUrl += "/account/confirm?id=" + confirmationToken + "&userId=" + System.Net.WebUtility.UrlEncode(newUser.Id);
                        await this._signInManager.SignInAsync(newUser, false);
                        var emailResult = await this._emailService.SendEmailAsync(
                            model.email,
                            "Welcome to BikeStore!",
                             "<p>Thanks for signing up, " + model.userName + "!</p><p><a href=\"" + confirmationUrl + "\">Confirm your account<a></p>",
                             "Thanks for signing up, " + model.userName + "!"
                            );

                        if (!emailResult.Success)
                        {
                            throw new Exception(string.Join(',', emailResult.Errors.Select(x => x.Message)));
                        }
                        return RedirectToAction("Index", "Home");
                    }
                
                else
                {
                    foreach (var error in creationResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
                else
            {
                foreach (var error in creationResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
        }
            return View();
        }


        public IActionResult SignOut()
        {
            this._signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(Models.SignInViewModel model)
        {

            if (ModelState.IsValid)
            {
                TCPUser existingUser = this._signInManager.UserManager.FindByNameAsync(model.email).Result;
                if (existingUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult passwordResult = this._signInManager.CheckPasswordSignInAsync(existingUser, model.password, false).Result;
                    if (passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("PasswordIncorrect", "Username or Password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserDoesNotExist", "Username or Password is incorrect.");

                }
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if ((ModelState.IsValid) && (!string.IsNullOrEmpty(email)))
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var resetToken = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);

                    resetToken = System.Net.WebUtility.UrlEncode(resetToken);
                    //using Microsoft.AspNetCore.Http.Extensions;
                    string currentUrl = Request.GetDisplayUrl();    //This will get me the URL for the current request
                    System.Uri uri = new Uri(currentUrl);   //This will wrap it in a "URI" object so I can split it into parts
                    string resetUrl = uri.GetLeftPart(UriPartial.Authority); //This gives me just the scheme + authority of the URI
                    resetUrl += "/account/resetpassword?id=" + resetToken + "&userId=" + System.Net.WebUtility.UrlEncode(user.Id);

                    string htmlContent = "<a href=\"" + resetUrl + "\">Reset your password</a>";
                    var emailResult = await _emailService.SendEmailAsync(email, "Reset your password", htmlContent, resetUrl);
                    if (!emailResult.Success)
                    {
                        throw new Exception(string.Join(',', emailResult.Errors.Select(x => x.Message)));

                    }
                    return RedirectToAction("ResetSent");
                }
            }
            ModelState.AddModelError("email", "Email is not valid");
            return View();
        }

        public IActionResult ResetSent()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, string userId, string password)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _signInManager.UserManager.ResetPasswordAsync(user, id, password);
                return RedirectToAction("SignIn");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Confirm(string id, string userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _signInManager.UserManager.ConfirmEmailAsync(user, id);
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();


        }

    }
}