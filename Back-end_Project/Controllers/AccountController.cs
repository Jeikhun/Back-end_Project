using Back_end_Project.ViewModels;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Back_end_Project.Constants;
using Newtonsoft.Json;
using Back_end_Project.Services.Interfaces;
using NuGet.Common;
using Back_end_Project.Helpers.EmailService.EmailSender;
using Back_end_Project.Helpers.EmailService.EmailSender.Abstract;
using System.Net.Mail;
using System.Net;

namespace Back_end_Project.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
			_emailSender = mailService;
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
                Email = model.Email.ToLower(),
                UserName = model.Username,
                PhoneNumber = "",
                //Fullname = model.Fullname
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



			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			//var confirmationLink = Url.Action(nameof(ConfirmEmail), "account", new { token, email = user.Email }, Request.Scheme);
			var confirmationLink = Url.Action(action: "confirmemail", controller: "account", values: new { token = token, email = user.Email }, protocol: Request.Scheme);
            //var message = new Message(new string[] { user.Email }, "Email Confirmation", confirmationLink);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("jeikhunjalil@gmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{confirmationLink}'>Confirm Email</a>";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential("jeikhunjalil@gmail.com", "fdgxcltipvqqujug");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);
            //_emailSender.SendEmail(message);

			await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
			TempData["register"] = "Please,verify your email";




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
                return View();
            }
            var user = await _userManager.FindByEmailAsync(model.Username);
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
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
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

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) 
            {
                NotFound();
            }

            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction(nameof(Login));





        }
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            string UserName = User.Identity.Name;
            User user = await _userManager.FindByNameAsync(UserName);
            AccountRegisterVM model = new AccountRegisterVM();
            model.Username = user.UserName;
            model.Email = user.Email;
            return View(model);
        }
        //private void CreateMessage(string message, string alerttype)
        //{
        //    var msg = new AlertMessage()
        //    {
        //        Message = message,
        //        AlertType = alerttype
        //    };
        //    TempData["message"] = JsonConvert.SerializeObject(msg);
        //}
        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

			[HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
              return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) { return NotFound(); }
            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Port = 7015;
            var result = Url.Action(action: "resetpassword", controller: "account", values: new { token = token.Result, email = email }, protocol: Request.Scheme);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("jeikhunjalil@gmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{result}'>Click here</a>";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential("jeikhunjalil@gmail.com", "fdgxcltipvqqujug");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);

            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token,string email)
        {
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) { return NotFound(); }
            ResetPasswordVM model = new ResetPasswordVM
            {
                Tokenn = token,
                Email= email
            };
            return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) { return NotFound(); }

            var result =await _userManager.ResetPasswordAsync(user, model.Tokenn, model.Password);
            if(!result.Succeeded)
            {
                return Json(result.Errors);
            }



            return RedirectToAction(nameof(Login));
        }
    }
}
