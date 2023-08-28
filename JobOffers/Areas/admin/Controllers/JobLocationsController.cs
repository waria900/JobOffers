using Bl;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class JobLocationsController : Controller
    {

        IjobLocations _oClsJobLocations;
        public JobLocationsController(IjobLocations oClsJobLocations)
        {
            _oClsJobLocations = oClsJobLocations;
        }

        public IActionResult List()
        {
            var listJobLocations = new List<TbJobLocation>();

            if (_oClsJobLocations.GetAll().Count > 0)
            {
                listJobLocations = _oClsJobLocations.GetAll();
            }

            return View(listJobLocations);
        }
        public IActionResult Edit(int? id)
        {
            var jobLocation = new TbJobLocation();
            if (id != null)
            {
                jobLocation = _oClsJobLocations.GetById(Convert.ToInt32(id));
            }
            return View(jobLocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbJobLocation jobLocation)
        {
            if (!ModelState.IsValid)
                return View("Edit", jobLocation);


            _oClsJobLocations.Save(jobLocation);

            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {

            _oClsJobLocations.Delete(id);

            return RedirectToAction("List");
        }
    }
}
