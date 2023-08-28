using Domains;

namespace JobOffers.Models
{
    public class VwHomeModel
    {
        public VwHomeModel() 
        {

            listJobs = new List<VwJob>();
            listCategories = new List<TbCategory>();
        }

        public List<VwJob> listJobs { get; set; }

        public List<TbCategory> listCategories { get; set; }


    }
}
