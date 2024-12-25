using System.ComponentModel.DataAnnotations;

namespace Bookify.Api.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

	    [Display(Name = "FirstName")]
	    public string FirstName { get; set; }

	    [Display(Name = "LastName")]
	    public string Lastname { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

	    [Display(Name = "City")]
	    public string City { get; set; }
    }
}