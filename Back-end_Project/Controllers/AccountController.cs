using Back_end_Project.ViewModels;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Back_end_Project.Constants;
using Newtonsoft.Json;

namespace Back_end_Project.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            //var code = _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var url = Url.Action("ConfirmEmail", "Account", new
            //{
            //    userId = user.Id,
            //    token = code
            //});
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
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Hesabiniz tesdiqlenmeyib");
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
            return RedirectToAction("index", "home");

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home", new { area = "" });
            //return Redirect("~/");

        }
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Xeta bash verdi.");
        //        return View();
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            ModelState.AddModelError(string.Empty, "Hesabiniz tesdiqlendi");
        //            return View();
        //        }
        //    }
        //    ModelState.AddModelError(string.Empty, "Hesab tesdiqlenmedi");
        //    return View();
        //}

        //private void CreateMessage(string message, string alerttype)
        //{
        //    var msg = new AlertMessage()
        //    {
        //        Message = message,
        //        AlertType = alerttype
        //    };
        //    TempData["message"] = JsonConvert.SerializeObject(msg);
        //}
    }
}
