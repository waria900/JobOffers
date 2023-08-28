using Microsoft.AspNetCore.Mvc;
using Bl;
using Domains;

namespace JobOffers.Controllers
{
    public class JobDetailsController : Controller
    {
        IJobs _oClsJobs;
        public JobDetailsController(IJobs oClsJobs) 
        {
            _oClsJobs = oClsJobs;
        }
        
        public IActionResult VwJobDetails(int JobId)
        {

            var jobDetails = _oClsJobs.GetJobById(JobId);
            return View(jobDetails);
        }
    }
}
