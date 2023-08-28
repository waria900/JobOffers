using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains;

public partial class TbCategory
{
    [ValidateNever]
    public int CategoryId { get; set; }

    [Required(ErrorMessage ="Please Enter Category Name")]
    public string CategoryName { get; set; } = null!;

    [ValidateNever]
    public string ImageName { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; }

    public virtual ICollection<TbJob> TbJobs { get; set; } = new List<TbJob>();
}
