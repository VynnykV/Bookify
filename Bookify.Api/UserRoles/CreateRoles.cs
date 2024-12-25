using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookify.Data;

namespace Bookify.Api.UserRoles
{
    public class CreateRoles
    {
        public static void Execute()
        {
            using (var context = new AuthContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole() { Name = "Admin" };
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole() { Name = "User" };
                    roleManager.Create(role);
                }
                context.SaveChanges();
            }
        }
    }
}