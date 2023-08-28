using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Bl;
using Domains;
using JobOffers.Utilities;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class JobsController : Controller
    {

        IJobs _oClsJobs;

        IApplyForJob _oClsApplyForJob;

        public JobsController(IJobs oClsJobs,  IApplyForJob oClsApplyForJob)
        {
            _oClsJobs = oClsJobs;
            _oClsApplyForJob = oClsApplyForJob;
        }


        public IActionResult list()
        {

            var listJobs = new List<VwJob>();
            if(_oClsJobs.GetAllData().Count() > 0) 
            {
                listJobs = _oClsJobs.GetAllData();
            }
            return View(listJobs);
        }


        public IActionResult Delete(int id) 
        {
            _oClsJobs.Delete(id);

            return RedirectToAction("List");
        }

        public IActionResult JobApplicants(int id) 
        {
          var jobs =  _oClsApplyForJob.GetJobById(id);
            return View(jobs);
        }


        public IActionResult DownloadFile(int applyId)
        {
            var values = _oClsApplyForJob.GetById(applyId);
            var fileName = values.PdfResume;
            // File name.              // File path.
            var memory = Helper.DownloadSinghFile(fileName, "wwwroot\\Downloads\\Resumes");
            //File type.       
            return File(memory.ToArray(), "application/pdf", fileName);
        }

    }
}
