using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Bookify.Data;
using Bookify.Data.Models;
using Bookify.Domain.DTOs;
using MoreLinq;
using System.Data;
using System.Threading.Tasks;


namespace Bookify.Domain.Repositories
{
    public class BooksRepository
    {
        private readonly int _numberOfBooksPerSearchQuery = int.Parse(ConfigurationManager.AppSettings["numberOfBooksPerSearchQuery"]);

        public List<Book> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
                IQueryable<Book> searchResultsQuery;

                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    // Якщо searchQuery порожній, беремо всі книги
                    searchResultsQuery = context.Books
                        .OrderBy(x => x.Title) // Сортуємо по назві
                        .Include(x => x.Author)
                        .Include(x => x.Category)
                        .Include(x => x.Genres);
                }
                else
                {
                    // Якщо searchQuery не порожній, виконуємо пошук за назвою
                    searchResultsQuery = context.Books
                        .Where(x => x.Title.Contains(searchQuery)) // Пошук за назвою
                        .OrderBy(x => x.Title)
                        .Include(x => x.Author)
                        .Include(x => x.Category)
                        .Include(x => x.Genres);
                }

                var isSameFilterRemoved = false;

                if (selectedFilters != null && selectedFilters.Contains("Other"))
                {
                    selectedFilters.Remove("Other");
                    isSameFilterRemoved = true;
                }

                // Застосовуємо фільтри
                if (selectedFilters != null && selectedFilters.Any())
                {
                    // Фільтр по авторам
                    if (selectedFilters.Intersect(context.Authors.Select(a => a.FirstName + " " + a.LastName)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book =>
                            selectedFilters.Contains(book.Author.FirstName + " " + book.Author.LastName));
                    }

                    // Фільтр по жанрам
                    if (selectedFilters.Intersect(context.Genres.Select(g => g.Name)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book =>
                            book.Genres.Any(genre => selectedFilters.Contains(genre.Name)));
                    }

                    // Фільтр по категоріям
                    if (selectedFilters.Intersect(context.Categories.Select(c => c.Name)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book =>
                            selectedFilters.Contains(book.Category.Name));
                    }

                    // Фільтр по мовам
                    if (selectedFilters.Intersect(context.Books.Select(b => b.Language)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book =>
                            selectedFilters.Contains(book.Language));
                    }
                }

                if (isSameFilterRemoved)
                {
                    selectedFilters.Add("Other");
                    searchResultsQuery = searchResultsQuery
                        .Where(book => book.Genres.Any(genre => selectedFilters.Contains(genre.Name) || selectedFilters.Contains(book.Category.Name)));
                }

                // Пагінація: Пропускаємо книги з попередніх сторінок і беремо потрібну кількість
                var books = searchResultsQuery
                    .Skip(_numberOfBooksPerSearchQuery * pageNumber)
                    .Take(_numberOfBooksPerSearchQuery)
                    .ToList();

                return books;
            }
        }


        public List<List<List<string>>> GetAllFilters(string searchQuery, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
                IQueryable<Book> searchResultsQuery;

                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    // Якщо searchQuery порожній, беремо всі книги
                    searchResultsQuery = context.Books
                        .OrderBy(x => x.Title)
                        .Include(x => x.Author)
                        .Include(x => x.Category)
                        .Include(x => x.Genres);
                }
                else
                {
                    // Якщо searchQuery не порожній, виконуємо пошук за назвою
                    searchResultsQuery = context.Books
                        .Where(x => x.Title.Contains(searchQuery))
                        .OrderBy(x => x.Title)
                        .Include(x => x.Author)
                        .Include(x => x.Category)
                        .Include(x => x.Genres);
                }

                // Застосовуємо вибрані фільтри
                if (selectedFilters != null && selectedFilters.Any())
                {
                    // Author Filter
                    if (selectedFilters.Intersect(context.Authors.Select(a => a.FirstName + " " + a.LastName)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Author.FirstName + " " + book.Author.LastName));
                    }

                    // Genre Filter
                    if (selectedFilters.Intersect(context.Genres.Select(g => g.Name)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book => book.Genres.Any(genre => selectedFilters.Contains(genre.Name)));
                    }

                    // Category Filter
                    if (selectedFilters.Intersect(context.Categories.Select(c => c.Name)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Category.Name));
                    }

                    // Language Filter
                    if (selectedFilters.Intersect(context.Books.Select(b => b.Language)).Any())
                    {
                        searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Language));
                    }
                }

                var allFilters = new List<List<List<string>>>
                {
                    GetFilters(searchResultsQuery, "Author"),
                    GetFilters(searchResultsQuery, "Genre"),
                    GetFilters(searchResultsQuery, "Category"),
                    GetFilters(searchResultsQuery, "Language")
                };

                return allFilters;
            }
        }

        public List<List<string>> GetFilters(IQueryable<Book> searchResultsQuery, string filterName)
        {
            var filterCount = new Dictionary<string, int>();

            if (filterName == "Author")
            {
                var filtersInBooks = searchResultsQuery
                    .Where(book => book.Author != null)
                    .Select(book => book.Author.FirstName + " " + book.Author.LastName)
                    .ToList();

                foreach (var filter in filtersInBooks)
                {
                    if (!filterCount.ContainsKey(filter))
                        filterCount[filter] = 1;
                    else
                        filterCount[filter]++;
                }
            }
            else if (filterName == "Genre")
            {
                var filtersInBooks = searchResultsQuery
                    .Where(book => book.Genres.Any())
                    .SelectMany(book => book.Genres)
                    .Select(genre => genre.Name)
                    .ToList();

                foreach (var filter in filtersInBooks)
                {
                    if (!filterCount.ContainsKey(filter))
                        filterCount[filter] = 1;
                    else
                        filterCount[filter]++;
                }
            }
            else if (filterName == "Category")
            {
                var filtersInBooks = searchResultsQuery
                    .Where(book => book.Category != null)
                    .Select(book => book.Category.Name)
                    .ToList();

                foreach (var filter in filtersInBooks)
                {
                    if (!filterCount.ContainsKey(filter))
                        filterCount[filter] = 1;
                    else
                        filterCount[filter]++;
                }
            }
            else if (filterName == "Language")
            {
                var filtersInBooks = searchResultsQuery
                    .Select(book => book.Language)
                    .ToList();

                foreach (var filter in filtersInBooks)
                {
                    if (!filterCount.ContainsKey(filter))
                        filterCount[filter] = 1;
                    else
                        filterCount[filter]++;
                }
            }

            var listOfValuesAsStrings = filterCount.Values.Select(value => value.ToString()).ToList();

            return new List<List<string>>
            {
                filterCount.Keys.ToList(),       // Список назв фільтрів
                listOfValuesAsStrings            // Кількість кожного фільтра
            };
        }

        public List<Author> GetAllAuthors()
        {
            using (var context = new AuthContext())
            {
                return context.Authors.ToList();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var context = new AuthContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Genre> GetAllGenres()
        {
            using (var context = new AuthContext())
            {
                return context.Genres.ToList();
            }
        }

        public static string[] Convert(object input)
        {
            return input as string[];
        }

        public async Task<Book> GetBookById(long id)
        {
            using (var context = new AuthContext())
            {
                var book = await context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Genres)
                    .FirstOrDefaultAsync(b => b.Id == id);

                return book;
            }
        }

        public async Task<long> AddNewBook(Book newBook)
        {
            using (var context = new AuthContext())
            {
                
                // Check if the author already exists, if not, add the new author
                var author = context.Authors.FirstOrDefault(a => a.Id == newBook.AuthorId);
                if (author == null)
                {
                    throw new Exception("Author not found.");
                }

                // Check if the category exists
                var category = context.Categories.FirstOrDefault(c => c.Id == newBook.CategoryId);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                // Check if genres exist and map them to the book
                var genreIds = newBook.Genres.Select(g => g.Id).ToList();
                var genres = context.Genres.Where(g => genreIds.Contains(g.Id)).ToList();
                if (genres.Count != genreIds.Count)
                {
                    throw new Exception("Some genres not found.");
                }

                newBook.Author = author;
                newBook.Category = category;
                newBook.Genres = genres;

                var createdBook = context.Books.Add(newBook);
                await context.SaveChangesAsync();

                return createdBook.Id;
            }
        }

        public async Task UpdateBook(Book updatedBook)
        {
            using (var context = new AuthContext())
            {
                var existingBook = context.Books
                    .Include(b => b.Genres)
                    .FirstOrDefault(b => b.Id == updatedBook.Id);

                if (existingBook == null)
                {
                    throw new Exception("Book not found.");
                }

                // Check if the author exists
                var author = context.Authors.FirstOrDefault(a => a.Id == updatedBook.AuthorId);
                if (author == null)
                {
                    throw new Exception("Author not found.");
                }

                // Check if the category exists
                var category = context.Categories.FirstOrDefault(c => c.Id == updatedBook.CategoryId);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                // Check if genres exist and map them to the book
                var genreIds = updatedBook.Genres.Select(g => g.Id).ToList();
                var genres = context.Genres.Where(g => genreIds.Contains(g.Id)).ToList();
                if (genres.Count != genreIds.Count)
                {
                    throw new Exception("Some genres not found.");
                }

                // Update book properties
                existingBook.Title = updatedBook.Title;
                existingBook.Language = updatedBook.Language;
                existingBook.PageCount = updatedBook.PageCount;
                existingBook.Description = updatedBook.Description;
                existingBook.Author = author;
                existingBook.Category = category;
                existingBook.Genres.Clear();
                existingBook.Genres.AddRange(genres);

                if (!string.IsNullOrEmpty(updatedBook.FilePath))
                    existingBook.FilePath = updatedBook.FilePath;

                if (!string.IsNullOrEmpty(updatedBook.CoverImagePath))
                    existingBook.CoverImagePath = updatedBook.CoverImagePath;

                await context.SaveChangesAsync();
            }
        }
    }
}