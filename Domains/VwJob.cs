using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwJob
    {

        public int JobId { get; set; }

        public string JobName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string CompanyDetails { get; set; } = null!;

        public string CompanyAddress { get; set; } = null!;

        public string CompanyLogo { get; set; } = null!;

        public string CompanyWebsite { get; set; } = null!;

        public string CompanyEmail { get; set; } = null!;

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public string JobDescription { get; set; } = null!;

        public string RequiredKnowledge { get; set; } = null!;

        public string Education { get; set; } = null!;

        public DateTime? PostedDate { get; set; }

        public int Vacancy { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int CurrentState { get; set; }

        public string UserId { get; set; } = null!;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; } = null!;

        public int JobLocationId { get; set; }
        public string JobLocationName { get; set; } = null!;
    }
}
