using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> List()
        {

            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }


        public async Task<IActionResult> Delete(string id)
        {


            var user = await _userManager.FindByIdAsync(id);
            if(user != null) 
            {
               var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) 
                {
                    return RedirectToAction("List");
                }

                foreach(var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.UserId = $"User with id {user} can not be found";
                return RedirectToAction("List");
            }

            

        }

    }
}
