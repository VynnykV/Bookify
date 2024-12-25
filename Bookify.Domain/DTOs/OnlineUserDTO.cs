using Bookify.Data.Models;
using System;

namespace Bookify.Domain.DTOs
{
    public class OnlineUserDTO
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string FavoriteGenres { get; set; }
        public string FavoriteWorks { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Viber { get; set; }


        public static OnlineUserDTO FromData(OnlineUser onlineUser)
		{
			return new OnlineUserDTO()
			{
				FirstName = onlineUser.FirstName,
                LastName = onlineUser.LastName,
                Email = onlineUser.Email,
                Username = onlineUser.UserName,
                Country = onlineUser.Country,
                City = onlineUser.City,
                LastLoginTime = onlineUser.LastLoginTime,
                RegistrationDate = onlineUser.RegistrationDate,
                FavoriteGenres = onlineUser.FavoriteGenres,
                FavoriteWorks = onlineUser.FavoriteWorks,
                Skype = onlineUser.Skype,
                Telegram = onlineUser.Telegram,
                Viber = onlineUser.Viber
			};
		}
	}
}
