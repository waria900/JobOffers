using Domains;
using JobOffers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobOffers.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var exists = await _userManager.FindByEmailAsync(user.Email);
                if (exists != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (exists != null && await _userManager.IsInRoleAsync(exists,"Admin") )
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
                }
                else
                {
                    TempData["error"] = "Enter a valid credentials";
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
                        var userRole = await _userManager.FindByIdAsync(user.Id);
                        if (model.UserType == "Publisher")  
                            await _userManager.AddToRoleAsync(userRole, "Publisher");               
                        else 
                            await _userManager.AddToRoleAsync(userRole, "Searcher");
               
                            

                        
                    }
                }


            }
            catch
            {

            }

            TempData["Congrants"] = "Registeration has succssfully completed😁👍";
            return View("Register");

            return View(new UsersModel());
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
