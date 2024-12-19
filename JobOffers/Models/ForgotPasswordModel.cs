using System.ComponentModel.DataAnnotations;

namespace JobOffers.Models
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Enter an email form.")]
        public string Email { get; set; } = "";
    }
}
