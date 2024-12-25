using Bookify.Data.Models;
using System;

namespace Bookify.Domain.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string Country { get; set; }

        public static AuthorDTO FromData(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Country = author.Country,
            };
        }
    }
}
