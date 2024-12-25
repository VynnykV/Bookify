using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Api.Models;
using Bookify.Data;
using Bookify.Data.Models;
using Bookify.Domain.DTOs;
using Bookify.Api.UserManager;

namespace Bookify.Api
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private ApplicationUserManager _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new ApplicationUserManager(new UserStore<OnlineUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new OnlineUser
            {
                FirstName = userModel.FirstName,
                LastName = userModel.Lastname,
                Country = userModel.Country,
                City = userModel.City,
                LastLoginTime = DateTime.UtcNow,
                RegistrationDate = DateTime.UtcNow,
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user.Id, "User");
            }
            
            return result;
        }

	    public async Task<bool> EditUserInfo(EditUserInfoModel editUserModel, Guid? userId)
	    {
		    var user = await _userManager.FindByIdAsync(userId.ToString());

            user.UserName = editUserModel.UserName;
            user.FirstName = editUserModel.FirstName;
            user.LastName = editUserModel.Lastname;
            user.Country = editUserModel.Country;
            user.City = editUserModel.City;
            user.FavoriteGenres = editUserModel.FavoriteGenres;
            user.FavoriteWorks = editUserModel.FavoriteWorks;
            user.Skype = editUserModel.Skype;
            user.Telegram = editUserModel.Telegram;
            user.Viber = editUserModel.Viber;
		    
		    return _ctx.SaveChanges() > 0;
	    }

        public async Task<bool> EditUserPassword(EditUserPasswordModel editUserPasswordModel, Guid? userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var isOkPassword = await _userManager.PasswordValidator.ValidateAsync(editUserPasswordModel.Password);
            if (isOkPassword.Succeeded)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(editUserPasswordModel.Password);
            }

            return _ctx.SaveChanges() > 0;
        }

        public async Task<OnlineUser> FindUser(string userNameOrEmail, string password)
        {
            OnlineUser user = null;

            if (userNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(userNameOrEmail);
            }

            return user;
        }

        public async Task<bool> CheckPasswordAsync(OnlineUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }


        public string GetUserRole(string userId)
        {
            using (var context = new AuthContext())
            {

                var roleId = context.Users.FirstOrDefault(user => user.Id == userId).Roles.FirstOrDefault().RoleId;

                return context.Roles.FirstOrDefault(role => role.Id == roleId).Name;
            }
        }

		public OnlineUserDTO GetUser(Guid? userId)
		{
			using (var context = new AuthContext())
			{
				var user = context.Users
                    .SingleOrDefault(onlineUser => onlineUser.Id == userId.ToString());

				return OnlineUserDTO.FromData(user);
			}


		}

		public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

	}
}