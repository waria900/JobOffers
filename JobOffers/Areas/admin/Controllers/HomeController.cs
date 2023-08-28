using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Bl;
using Domains;
using Microsoft.AspNetCore.Identity;
using JobOffers.Areas.admin.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class HomeController : Controller
    {
        IJobs _oClsJobs;
        ICategories _oClsCategories;
        IJobTypes _oClsJobTypes;
        IjobLocations _oClsJobLocations;
        IApplyForJob _oClsApplyForJob;
        JobOffersContext _ctx;
        public HomeController(IJobs oClsJobs,ICategories oClsCategories,IJobTypes oClsJobTypes,IjobLocations oClsJobLocations,IApplyForJob oClsApplyForJob, JobOffersContext ctx) 
        {
            _oClsJobs = oClsJobs;
            _oClsCategories = oClsCategories;
            _oClsJobTypes = oClsJobTypes;
            _oClsJobLocations = oClsJobLocations;
            _oClsApplyForJob = oClsApplyForJob;
            _ctx = ctx;
            
        }

        public IActionResult Index()
        {
            VwDashboardPage vw = new VwDashboardPage();
            vw.NumOfApplicants = _oClsApplyForJob.GetAllData().Count();
            vw.NumOfJobs = _oClsJobs.GetAllData().Count();
            vw.NumOfJobLocations = _oClsJobLocations.GetAll().Count();
            vw.NumOfJobTypes = _oClsJobTypes.GetAll().Count();
            vw.NumOfCategories = _oClsCategories.GetAll().Count();
            vw.NumOfUsers = _ctx.Users.Count();

            return View(vw);
        }
    }
}
