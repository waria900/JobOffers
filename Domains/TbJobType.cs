using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains;

public partial class TbJobType
{
    [ValidateNever]
    public int JobTypeId { get; set; }
    [Required(ErrorMessage = "Please Enter Job Type Name")]
    public string JobTypeName { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; }

    public virtual ICollection<TbJob> TbJobs { get; set; } = new List<TbJob>();
}
