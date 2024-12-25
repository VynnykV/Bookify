using System.ComponentModel.DataAnnotations;

namespace Bookify.Api.Models
{
    public class EditUserInfoModel
    {
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
        public string FavoriteGenres { get; set; }
        public string FavoriteWorks { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Viber { get; set; }
    }
}
