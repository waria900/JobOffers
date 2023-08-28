using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace JobOffers.Models
{
    public class UsersModel
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string secondName { get; set; } = null!;


        [Required(ErrorMessage = "Please Enter ThirdName")]
        public string ThirdName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; } = null!;


        public string UserType { get; set; } = null!;



        [ValidateNever]
        public string ReturnUrl { get; set; } = null!;
        
    }
}
