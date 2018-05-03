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