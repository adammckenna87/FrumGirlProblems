using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrumGirlProblems.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<IdentityUser> _signInManager;
        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
           
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

        public IActionResult SignOut()
        {
            this._signInManager.SignOutAsync().Wait();
            return View("Index", "Home");
        }

        public IActionResult SignIn()
        {
            return View();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(Models.SignInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.email,
                    model.password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
            
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Models.RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser(model.email);

                IdentityResult creationResult = this._signInManager.UserManager.CreateAsync(newUser).Result;
                if (creationResult.Succeeded)
                {
                    //TODO: Create an account and log this user in;
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
            return View();


        
        }
    }
}