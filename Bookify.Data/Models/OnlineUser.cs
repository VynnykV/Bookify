using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Data.Models
{
    public class OnlineUser : IdentityUser
    {
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string FavoriteGenres { get; set; }
        public string FavoriteWorks { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Viber { get; set; }
    }
}
