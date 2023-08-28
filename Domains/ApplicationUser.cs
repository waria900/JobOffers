using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() 
        {

            TbApplyForAJobs = new HashSet<TbApplyForAJob>();
        }

        public string firstName { get; set; } = null!;
        public string secondName { get; set; } = null!;
        public string thirdName { get; set; } = null!;

       

        public ICollection<TbApplyForAJob> TbApplyForAJobs { get; set; }
    }
}
