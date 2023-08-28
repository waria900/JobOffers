using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwApplyForAJob
    {

        public int ApplyId { get; set; }

        public string? Message { get; set; }

        public DateTime ApplyDate { get; set; }

        public string PdfResume { get; set; } = null!;

        public int CurrentState { get; set; }

        public int JobId { get; set; }

        public string UserId { get; set; } = null!;


        public string firstName { get; set; } = null!;
        public string secondName { get; set; } = null!;



        public string JobName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string CompanyLogo { get; set; } = null!;
    }
}
