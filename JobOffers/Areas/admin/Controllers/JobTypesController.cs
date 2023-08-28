using Bl;
using Domains;
using JobOffers.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class JobTypesController : Controller
    {

        IJobTypes _oClsJobTypes;
        public JobTypesController(IJobTypes oClsJobTypes) 
        {
            _oClsJobTypes = oClsJobTypes;
        }

        public IActionResult List()
        {
            var listJobTypes = new List<TbJobType>();

            if (_oClsJobTypes.GetAll().Count > 0)
            {
                listJobTypes = _oClsJobTypes.GetAll();
            }

            return View(listJobTypes);
        }
        public IActionResult Edit(int? id)
        {
            var JobType = new TbJobType();
            if (id != null)
            {
                JobType = _oClsJobTypes.GetById(Convert.ToInt32(id));
            }
            return View(JobType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbJobType JobType)
        {
            if (!ModelState.IsValid)
                return View("Edit", JobType);



            _oClsJobTypes.Save(JobType);

            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {

            _oClsJobTypes.Delete(id);

            return RedirectToAction("List");
        }
    }
}
