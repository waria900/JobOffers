using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domains;

public partial class TbJob
{

    public TbJob() 
    {
        TbApplyForAJobs = new HashSet<TbApplyForAJob>();
    }
    public int JobId { get; set; }
    [Required(ErrorMessage = "Please Enter Job Name")]
    [MaxLength(20, ErrorMessage = "The Job Name field cannot exceed 20 characters.")]
    public string JobName { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Company Name")]
    [MaxLength(20, ErrorMessage = "The Company Name field cannot exceed 20 characters.")]
    public string CompanyName { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Company Details")]
    public string CompanyDetails { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Company Address")]
    [MaxLength(20, ErrorMessage = "The Company Address field cannot exceed 20 characters.")]
    public string CompanyAddress { get; set; } = null!;
    [ValidateNever]
    public string CompanyLogo { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Company Website")]
    public string CompanyWebsite { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Eompany Email")]
    public string CompanyEmail { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Minimum Salary")]
    public decimal MinSalary { get; set; }
    [Required(ErrorMessage = "Please Enter Maximum Salary")]
    public decimal MaxSalary { get; set; }
    [Required(ErrorMessage = "Please Enter Job Description")]
    [AllowHtml]
    public string JobDescription { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Required Knowledge")]
    [AllowHtml]
    public string RequiredKnowledge { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Education")]
    [AllowHtml]
    public string Education { get; set; } = null!;
    [ValidateNever]
    public DateTime? PostedDate { get; set; }
    [Required(ErrorMessage = "Please Enter Vacancy")]
    public int Vacancy { get; set; }
    [Required(ErrorMessage = "Please Enter Application Date")]
    public DateTime ApplicationDate { get; set; }

    public int CurrentState { get; set; }
    [ValidateNever]
    public string UserId { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Category")]
    public int CategoryId { get; set; }
    [ValidateNever]
    public int JobTypeId { get; set; }
    [Required(ErrorMessage = "Please Enter Location")]
    public int JobLocationId { get; set; }
    [ValidateNever]
    public virtual TbCategory Category { get; set; } = null!;
    [ValidateNever]
    public virtual TbJob CategoryNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<TbJob> InverseCategoryNavigation { get; set; } = new List<TbJob>();
    [ValidateNever]
    public virtual TbJobLocation JobLocation { get; set; } = null!;
    [ValidateNever]
    public virtual TbJobType JobType { get; set; } = null!;



    [ValidateNever]
    public ICollection<TbApplyForAJob> TbApplyForAJobs { get; set; }
}
