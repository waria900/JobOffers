using Bl;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobOffers.Utilities;
using System.Security.Claims;

namespace JobOffers.Controllers
{
    [Authorize(Roles ="Publisher")]
    public class JobsController : Controller
    {
        IJobs _oClsJobs;
        ICategories _oClsCategories;
        IJobTypes _oClsJobTypes;
        IjobLocations _oClsJobLocations;
        IApplyForJob _oClsApplyForJob;

        public JobsController(IJobs oClsJobs, ICategories oClsCategories,IJobTypes oClsJobTypes,IjobLocations oClsJobLocations, IApplyForJob oClsApplyForJob) 
        {
            _oClsJobs = oClsJobs;
            _oClsCategories = oClsCategories;
            _oClsJobTypes = oClsJobTypes;
            _oClsJobLocations = oClsJobLocations;
            _oClsApplyForJob = oClsApplyForJob;
        }

        public IActionResult list()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listJobs = new List<VwJob>();
            if (_oClsJobs.GetUserById(userId).Count() > 0)
            {
                listJobs = _oClsJobs.GetUserById(userId);
            }
            return View(listJobs);
        }

        public IActionResult Edit(int? id)
        {
            var job = new TbJob();
            ViewBag.listCategories = _oClsCategories.GetAll();
            ViewBag.listJobTypes = _oClsJobTypes.GetAll();
            ViewBag.listJobLocations = _oClsJobLocations.GetAll();
            if (id != null)
            {
                job = _oClsJobs.GetById(Convert.ToInt32(id));
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbJob job , List<IFormFile> Images) 
        {
            if (!ModelState.IsValid)
                return View("Edit",job);

                job.CompanyLogo = Images.Count() == 0 ? job.CompanyLogo : job.CompanyLogo = await Helper.UploadImage(Images, "Jobs");



            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            job.UserId = userId;


            _oClsJobs.Save(job);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            _oClsJobs.Delete(id);

            return RedirectToAction("List");
        }

        public IActionResult JobApplicants(int id)
        {
           var job = _oClsApplyForJob.GetJobById(id);

            return View(job);
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
