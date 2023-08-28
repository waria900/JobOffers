using Bl;
using JobOffers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobOffers.Controllers
{
    public class HomeController : Controller
    {
        IJobs _oClsJobs;
        ICategories _oClsCategories;
        IjobLocations _oClsJobLocations;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IJobs oClsJobs, ICategories oClsCategories, IjobLocations oClsJobLocations)
        {
            _logger = logger;
            _oClsJobs = oClsJobs;
            _oClsCategories = oClsCategories;
            _oClsJobLocations = oClsJobLocations;
        }

        public List<string> GetCategoryPath(int categoryId) 
        {
            var category = _oClsCategories.GetById(categoryId);

            return new List<string> { category.CategoryName , category.ImageName, category.CategoryId.ToString() };
        }


        public IActionResult Index()
        {
            VwHomeModel ViewHome= new VwHomeModel();
            ViewHome.listJobs = _oClsJobs.GetAllData().Skip(7).Take(4).ToList();
            

            var categoryPaths = _oClsJobs.GetAllData().
                GroupBy(x => x.CategoryId).
                Select(x => new CategoryCountHomeView
                {
                    CategoryName = GetCategoryPath(x.Key)[0],
                    ImagePath = GetCategoryPath(x.Key)[1],
                    Id = Convert.ToInt32(GetCategoryPath(x.Key)[2]),
                    CategoryCount = x.Count()
                });

            ViewBag.CategoryPaths = categoryPaths;

            ViewBag.listLocations = _oClsJobLocations.GetAll();

            return View(ViewHome);
        }





        public IActionResult AboutUs() 
        {


            return View();
        }


        public IActionResult ContactUs()
        {


            return View();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}