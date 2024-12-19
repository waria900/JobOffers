using Domains;
using JobOffers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Bl;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using NETCore.MailKit.Core;
// using NETCore.MailKit.Core;

namespace JobOffers.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        // IEmailSender _oClsEmailConfirm;
        
        //MailKil to send email;
        IEmailService _emailSender;
        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_oClsEmailConfirm = oClsEmailConfirm;
            _emailSender = emailSender;
        }
        public IActionResult Login(string ReturnUrl)
        {
            UsersModel model = new UsersModel() 
            {
                ReturnUrl= ReturnUrl,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsersModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email= model.Email,
                UserName=model.Email
            };

            try
            {
               

                // For Email Confirmation.

                

                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);
               
                var exists = await _userManager.FindByEmailAsync(user.Email);

                if (exists == null)
                {
                    TempData["Error"] = "Your email is not exists register now.";
                    return View("login");
                }

                bool emailConfirm = await _userManager.IsEmailConfirmedAsync(exists);
              
                if (exists != null && emailConfirm)
                {
                    if (result.Succeeded)
                    {
                  
                        if (exists != null && await _userManager.IsInRoleAsync(exists, "Admin"))
                        {
                            return Redirect("~/Admin");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(model.ReturnUrl))
                                return Redirect("~/");
                            else
                                return Redirect(model.ReturnUrl);
                        }

                  

                    }
                    else
                    {
                        TempData["Error"] = "Enter a valid credentials";
                        return View("login");
                    }

                }
                else
                {
                    TempData["ConfirmYourEmail"] = "Please confirm your email first.";
                    return View("login");
                }


            }
            catch 
            {
                
            }


            return View();
        }

        public IActionResult Register()
        {
            return View(new UsersModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsersModel model)
        {
            if(!ModelState.IsValid)
                return View("Register",model);

            ApplicationUser user = new ApplicationUser()
            {
                firstName= model.FirstName,
                secondName= model.secondName,
                UserName = model.Email,
                thirdName=model.ThirdName,
                Email=model.Email

            };


            try
            {
                var exist = await _userManager.FindByEmailAsync(user.Email);
                if (exist != null)
                {
                    TempData["error"] = "This is already exist.";
                }
                else
                {


                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                        var confirmLink = Url.Action("ConformEmail", "Users", new { userId = user.Id, token = token }, Request.Scheme);

                        await _emailSender.SendAsync(user.Email, "Confirm your email", $"<a href='{HtmlEncoder.Default.Encode(confirmLink)}'>Email Verify</a>",true);

                        // await _oClsEmailConfirm.SendEmailAsync(user.Email, "Confirm your email", $"<a href='{HtmlEncoder.Default.Encode(confirmLink)}'>Email Verify</a>");

                        var userRole = await _userManager.FindByIdAsync(user.Id);
                        if (model.UserType == "Publisher")
                            await _userManager.AddToRoleAsync(userRole, "Publisher");
                        else
                            await _userManager.AddToRoleAsync(userRole, "Searcher");


                        // For Email Confirmation.
                         /*    if (_signInManager.Options.SignIn.RequireConfirmedEmail)
                        
                          
                    */

                    }
                }


            }
            catch
            {

            }
            TempData["Title"] = "Email Confirmation";
            TempData["Message"] = "We have sent a confirmation to your e-mail for verification.";
            return View("Status");

            //return View(new UsersModel());
        }


        // For Email Confirmation.

        public IActionResult Status()
        {
            return View();
        }




        // For Email Confirmation.

        
        public async Task<IActionResult> ConformEmail(string userId, string token)
        {


            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {

                TempData["Title"] = "Error";
                TempData["Message"] = "Please check your enterd data.";

                return RedirectToAction("Status");
            }


            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) 
            {
                TempData["Title"] = "Error";
                TempData["Message"] = $"Unable to load user with ID '{userId}'.";
                return RedirectToAction("Status");
            }


            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user, token);

            TempData["Title"] = "Confirmation Successfully";
            TempData["Message"] = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email";
            

            return RedirectToAction("Status");

        }
        

        public IActionResult ForgotPassword()
        {

            return View(new ForgotPasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View("ForgotPassword", model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user)) 
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var resetLink = Url.Action("ResetPassword", "Users", new { Email = user.Email, token = token }, Request.Scheme);

                await _emailSender.SendAsync(user.Email, "Reset Your Password", $"Click here on<a href='{HtmlEncoder.Default.Encode(resetLink)}'>Email Verify</a> to reset you password", true);

                TempData["Title"] = "Check Your Email";
                TempData["Message"] = "We have sent a link on your email to reset your password.";

                return RedirectToAction("Status");
            }
            else 
            {
                TempData["Title"] = "Error";
                TempData["Message"] = "You might not a regular user, please register and confirm your email.";

                return RedirectToAction("Status");
            }

        }

        [HttpGet]
        public IActionResult ResetPassword(string Email, string token ) 
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null) 
            {
                model.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded) 
                {
                    TempData["Title"] = "Reset Successfully";
                    TempData["Message"] = "Your password has been reset.";
                    return RedirectToAction("status");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
            TempData["Title"] = "Error";
            TempData["Message"] = "We could not find your email addres.";
            return RedirectToAction("status");
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
