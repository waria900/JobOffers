using Bl;
using Domains;

namespace JobOffers.Models
{
    public class VwJobListing
    {
        public VwJobListing() 
        {
            lstAllJobs = new List<VwJob>();
            lstCategories = new List<TbCategory>();
            lstJobLocations = new List<TbJobLocation>();
            lstJobTypes = new List<TbJobType>();
        }

        public List<VwJob> lstAllJobs { get; set; }
        public List<TbCategory> lstCategories { get; set; }
        public List<TbJobLocation> lstJobLocations { get; set; }
        public List<TbJobType> lstJobTypes { get; set; }
        public int jobNums { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
