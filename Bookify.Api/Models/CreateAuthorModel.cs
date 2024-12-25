using Bookify.Data.Models;
using Bookify.Domain.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Api.Models
{
    public class CreateAuthorModel
    {
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string Country { get; set; }

        public static Author ToData(CreateAuthorModel createAuthorModel)
        {
            return new Author
            {
                FirstName = createAuthorModel.FirstName,
                LastName = createAuthorModel.LastName,
                Biography = createAuthorModel.Biography,
                BirthDate = createAuthorModel.BirthDate,
                DeathDate = createAuthorModel.DeathDate,
                Country = createAuthorModel.Country,
            };
        }

        public static AuthorDTO FromData(CreateAuthorModel createAuthorModel)
        {
            return new AuthorDTO
            {
                FirstName = createAuthorModel.FirstName,
                LastName = createAuthorModel.LastName,
                Biography = createAuthorModel.Biography,
                BirthDate = createAuthorModel.BirthDate,
                DeathDate = createAuthorModel.DeathDate,
                Country = createAuthorModel.Country
            };
        }
    }
}
