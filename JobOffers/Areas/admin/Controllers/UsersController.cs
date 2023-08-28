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
    }
}
