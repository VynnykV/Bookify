using System.Data.Entity;
using Bookify.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bookify.Data
{
    public class AuthContext : IdentityDbContext<OnlineUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .Map(m =>
                {
                    m.ToTable("BookGenres");
                    m.MapLeftKey("BookId");
                    m.MapRightKey("GenreId");
                });

            modelBuilder.Entity<Book>()
                .HasRequired(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasOptional(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}