using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class TbApplyForAJob
    {
        [ValidateNever]
        public int ApplyId { get; set; }
        [ValidateNever]
        public string? Message { get; set; }
        [ValidateNever]
        public DateTime ApplyDate { get; set; }
        [ValidateNever]
        public string PdfResume { get; set; } = null!;
        [ValidateNever]
        public int CurrentState { get; set; }
       
        
        [ValidateNever]
        public int JobId { get; set; }

        [ValidateNever]
        public virtual TbJob Job { get; set; } = null!;


        [ValidateNever]
        public string UserId { get; set; } = null!;

        [ValidateNever]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
