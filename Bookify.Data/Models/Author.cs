using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
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
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
