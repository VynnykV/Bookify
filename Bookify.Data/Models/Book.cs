using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Data.Models
{
    public class Book
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int PageCount { get; set; }
        public string CoverImagePath { get; set; }
        public string FilePath { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
	}
}
