using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrumGirlProblems.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrumGirlProblems.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<TCPUser> _signInManager;
        public AccountController(SignInManager<TCPUser> signInManager)
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
                TCPUser newUser = new TCPUser {
                    Email = model.email,
                    UserName = model.userName
                };

                IdentityResult creationResult = this._signInManager.UserManager.CreateAsync(newUser).Result;
                if (creationResult.Succeeded)
                {

                    IdentityResult passwordResult = this._signInManager.UserManager.AddPasswordAsync(newUser, model.password).Result;
                    if (passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(newUser, false).Wait();
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

    }
}