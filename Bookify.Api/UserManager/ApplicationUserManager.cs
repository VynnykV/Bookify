using Bookify.Data.Models;
using Microsoft.AspNet.Identity;

namespace Bookify.Api.UserManager
{
    public class ApplicationUserManager : UserManager<OnlineUser>
    {
        public ApplicationUserManager(IUserStore<OnlineUser> store)
            : base(store)
        {
            UserValidator = new UserValidator<OnlineUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireNonLetterOrDigit = false
            };
        }
    }
}