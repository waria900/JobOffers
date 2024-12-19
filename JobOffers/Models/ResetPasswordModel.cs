using System.ComponentModel.DataAnnotations;

namespace JobOffers.Models
{
    public class ResetPasswordModel
    {

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter email form.")]
        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and confirm password must be match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
