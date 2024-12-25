using Bookify.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Bookify.Domain.DTOs;

namespace Bookify.Domain.Repositories
{
    public class LibrariesRepository
    {
        public List<AdminBookListDTO> GetBookList(string sortOption, bool descending, string searchQuery, int pageNumber)
        {
            if (string.IsNullOrEmpty(searchQuery))
                searchQuery = "";

            using (var context = new AuthContext())
            {
                var booksQuery = context.Books
                    .Where(book => book.Title.Contains(searchQuery))
                    .AsQueryable();

                switch (sortOption)
                {
                    case "title":
                        booksQuery = descending
                            ? booksQuery.OrderByDescending(book => book.Title)
                            : booksQuery.OrderBy(book => book.Title);
                        break;
                    case "description":
                        booksQuery = descending
                            ? booksQuery.OrderByDescending(book => book.Description)
                            : booksQuery.OrderBy(book => book.Description);
                        break;
                    case "language":
                        booksQuery = descending
                            ? booksQuery.OrderByDescending(book => book.Language)
                            : booksQuery.OrderBy(book => book.Language);
                        break;
                    default:
                        booksQuery = booksQuery.OrderBy(book => book.Title);
                        break;
                }

                // Pagination
                var books = booksQuery
                    .Skip(pageNumber * 10)
                    .Take(10)
                    .ToList();

                // Convert to AdminBookListDTO
                var bookList = books.Select(book => new AdminBookListDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Language = book.Language
                }).ToList();

                return bookList;
            }
        }
    }
}