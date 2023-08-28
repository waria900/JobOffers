using Domains;

namespace JobOffers.Areas.admin.Models
{
    public class VwDashboardPage
    {

        //public VwDashboardPage() 
        //{
        //    NumOfApplicants = new List<ApplicationUser>();
        //}


        public int NumOfCategories { get; set; }
        public int NumOfJobLocations { get; set; }
        public int NumOfJobTypes { get; set; }
        public int NumOfJobs { get; set; }
        public int NumOfUsers { get; set; }
        public int NumOfApplicants { get; set; }

    }
}
