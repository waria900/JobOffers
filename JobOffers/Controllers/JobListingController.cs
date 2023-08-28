using Bl;
using JobOffers.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobOffers.Controllers
{
    public class JobListingController : Controller
    {


        IJobs _oClsJobs;
        ICategories _oClsCategories;
        IJobTypes _oClsJobTypes;
        IjobLocations _oClsJobLocations; 
        public JobListingController(IJobs oClsJobs, ICategories oClsCategories, IJobTypes oClsJobTypes, IjobLocations oClsJobLocations)
        {
            _oClsJobs = oClsJobs;
            _oClsCategories = oClsCategories;
            _oClsJobTypes = oClsJobTypes;
            _oClsJobLocations = oClsJobLocations;
            
        }




        public IActionResult List(int categoryId, List<int> jobTypesIds, int locationId, string salary, string postedDate, string jobNameText, int? pageNumber)
        {
            VwJobListing vm = new VwJobListing();
            var AllJobs = _oClsJobs.GetAllData();

            if (categoryId != 0)
            {
                AllJobs = AllJobs.Where(a => a.CategoryId == categoryId).ToList();
            }

            if (jobTypesIds.Count != 0)
            {
                AllJobs = AllJobs.Where(j => jobTypesIds.Contains(j.JobTypeId)).ToList();
            }

            if (locationId != 0)
            {
                AllJobs = AllJobs.Where(a => a.JobLocationId == locationId).ToList();
            }
            if (salary != null)
            {
                if (salary == "MaxLength")
                {
                    AllJobs = AllJobs.OrderByDescending(a => a.MaxSalary).ToList();
                }
                else if (salary == "MinLength")
                {
                    AllJobs = AllJobs.OrderBy(a => a.MinSalary).ToList();
                }

            }

            if (jobNameText != null)
            {

                AllJobs = AllJobs.Where(a => a.JobName.Contains(jobNameText)).ToList();

            }

            //vm.lstAllJobs = AllJobs.OrderBy(a=>a.PostedDate).ToList();
            vm.lstCategories = _oClsCategories.GetAll();
            vm.lstJobLocations = _oClsJobLocations.GetAll();
            vm.lstJobTypes = _oClsJobTypes.GetAll();
            vm.jobNums = AllJobs.Count();



            const int pageSize = 5;
            var dataCount = vm.jobNums;
            vm.TotalPages = (int)Math.Ceiling((double)dataCount / pageSize);
            vm.PageNumber = pageNumber ?? 1;
            if (vm.PageNumber < 1)
            {
                vm.PageNumber = 1;
            }
            if (vm.PageNumber > vm.TotalPages)
            {
                vm.PageNumber = vm.TotalPages;
            }

            vm.lstAllJobs = AllJobs.Skip((vm.PageNumber - 1) * pageSize).Take(pageSize).OrderByDescending(a => a.PostedDate).ToList();


            return View(vm);
        }
    }
}
