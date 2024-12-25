using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Bookify.Data;
using Bookify.Data.Models;

namespace Bookify.Api
{
    public class Seed
    {
        public static void Execute()
        {
            using (var context = new AuthContext())
            {
                var store = new UserStore<OnlineUser>(context);
                var _userManager = new UserManager<OnlineUser>(new UserStore<OnlineUser>(context));

                if (!context.Users.Any())
                {
                    var adminUser = new OnlineUser()
                    {
                        FirstName = "name",
                        LastName = "lastName",
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                        Country = "USA",
                        City = "Florida"
                    };

                    _userManager.Create(adminUser, "password123456");
                    _userManager.AddToRole(adminUser.Id, "Admin");

                    context.SaveChanges();
                }

                if (!context.Categories.Any())
                {
                    var categories = new[]
                    {
                        new Category { Name = "Fiction" },
                        new Category { Name = "Non-fiction" },
                        new Category { Name = "Children's Literature" },
                        new Category { Name = "Educational" },
                        new Category { Name = "Specialized Literature" },
                        new Category { Name = "Reference Books" },
                        new Category { Name = "Cooking and Hobbies" },
                        new Category { Name = "Other" }
                    };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }

                if (!context.Genres.Any())
                {
                    var genres = new[]
                    {
                        new Genre { Name = "Science Fiction" },
                        new Genre { Name = "Fantasy" },
                        new Genre { Name = "Detective" },
                        new Genre { Name = "Thriller" },
                        new Genre { Name = "Romance" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "Horror" },
                        new Genre { Name = "Historical Fiction" },
                        new Genre { Name = "Mystery" },
                        new Genre { Name = "Satire" },
                        new Genre { Name = "Psychological Fiction" },
                        new Genre { Name = "Social Fiction" },
                        new Genre { Name = "Epic Literature" },
                        new Genre { Name = "Literary Horror" },
                        new Genre { Name = "Dystopian" },
                        new Genre { Name = "Post-Apocalyptic" },
                        new Genre { Name = "Biographical Novel" },
                        new Genre { Name = "Literary Criticism" },
                        new Genre { Name = "Comedy" },
                        new Genre { Name = "Erotica" },
                        new Genre { Name = "Parody" },
                        new Genre { Name = "Military Literature" },
                        new Genre { Name = "Political Satire" },
                        new Genre { Name = "Other" }
                    };

                    context.Genres.AddRange(genres);
                    context.SaveChanges();
                }
            }
        }
    }
}