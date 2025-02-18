﻿using Microsoft.AspNetCore.Mvc;
using PhonebookApplication.Data;
using PhonebookApplication.Services.Contract;
using PhonebookApplication.ViewModels;

namespace PhonebookApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var message = _authService.RegisterUserService(register);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    TempData["ErrorMessage"] = message;
                    return View(register);
                }
                return RedirectToAction("RegisterSuccess");
            }
            return View(register);
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var message = _authService.LoginUserService(login);
                if (message == "Invalid username or passowrd")
                {
                    TempData["ErrorMessage"] = message;
                    return View(login);
                }
                else if (message == "Something went wrong, please try after some time.")
                {
                    TempData["ErrorMessage"] = message;
                    return View(login);
                }
                else
                {
                    string token = message;
                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                }
                return RedirectToAction("Index", "Contact");
            }
            return View(login);
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Index", "Home");
        }
    }
}
