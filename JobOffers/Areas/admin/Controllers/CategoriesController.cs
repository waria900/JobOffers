using Bl;
using Domains;
using Microsoft.AspNetCore.Mvc;
using JobOffers.Utilities;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace JobOffers.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class CategoriesController : Controller
    {
        ICategories _oClsCategories;
        public CategoriesController(ICategories oClsCategories)
        {
            _oClsCategories = oClsCategories;
        }

        public IActionResult List()
        {
            var listCategories = new List<TbCategory>();

            if(_oClsCategories.GetAll().Count > 0)
            {
                listCategories = _oClsCategories.GetAll();
            }

            return View(listCategories);
        }
        public IActionResult Edit(int? id)
        {
            var category = new TbCategory();
            if (id != null)
            {
                category = _oClsCategories.GetById(Convert.ToInt32(id));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Images)
        {
            if (!ModelState.IsValid)
                return View("Edit", category);

            category.ImageName = Images.Count() == 0 ? category.ImageName :  await Helper.UploadImage(Images,"Categories");

            _oClsCategories.Save(category);

            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {

            _oClsCategories.Delete(id);

            return RedirectToAction("List");
        }
    }
}
