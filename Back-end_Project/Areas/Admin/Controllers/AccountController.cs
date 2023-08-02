using Back_end_Project.Constants;
using Back_end_Project.Areas.Admin.ViewModels;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model); 
            }
            await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");
                return View(model);
            }
            if (!string.IsNullOrEmpty(model.ReturnUrl)&&Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("index", "dashboard");

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account", new { area = "admin" });

        }
    }
}
