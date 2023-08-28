using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using JobOffers.Utilities;
using System.Security.Claims;
using Domains;
using Bl;


namespace JobOffers.Controllers
{
    [Authorize(Roles = "Searcher")]
    public class ApplyForJobsController : Controller
    {

        JobOffersContext _ctx;
        IApplyForJob _oClsApplyForJob;
        public ApplyForJobsController(IApplyForJob oClsApplyForJob, JobOffersContext ctx) 
        {
            _oClsApplyForJob =  oClsApplyForJob;
            _ctx = ctx;
        }

        public IActionResult List()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return View(_oClsApplyForJob.GetUserById(userId));
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

        public IActionResult Edit(int jobId, int? applyId)
        {
            var apply = new TbApplyForAJob();

            if (applyId != null)
                apply = _oClsApplyForJob.GetById(Convert.ToInt32(applyId));
            else
                apply.JobId = jobId;

            return View(apply);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbApplyForAJob apply, List<IFormFile> File)
        {
            if (!ModelState.IsValid)
                return View("Edit", apply);

            apply.PdfResume = await Helper.UploadFile(File, "Resumes");

            apply.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if(apply.ApplyId == 0) {

                var check = _ctx.TbApplyForAJobs.Where(x => x.UserId == apply.UserId && x.JobId == apply.JobId && x.CurrentState == 1).ToList();
                if(check.Count() > 0)
                {
                    TempData["Error"] = "You have applied this job before.";
                    return RedirectToAction("edit");
                }
            }

            _oClsApplyForJob.Save(apply);
            return RedirectToAction("List");

           
        }

        public IActionResult Delete(int applyId)
        {
            _oClsApplyForJob.Delete(applyId);
            
            return RedirectToAction("list");
        }
    }
}
