using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Bookify.Data.Models;
using Bookify.Domain.DTOs;
using Bookify.Domain.Repositories;

namespace Bookify.Api.Controllers
{

    [RoutePrefix("api/search")]
    public class BooksController : AuthorizationController
    {
        private BooksRepository repo = null;

        public BooksController()
        {
            repo = new BooksRepository();
        }

        [HttpGet]
        [Route("byquery")]
        public List<Book> SearchBooks(int pageNumber, string searchQuery, [FromUri] List<string> selectedFilters)
        {
            return repo.SearchBooks(searchQuery, pageNumber, selectedFilters);
        }

        [HttpGet]
        [Route("get-filters")]
        public List<List<List<string>>> GetFiltersForSearch(string searchQuery, [FromUri] List<string> selectedFilters)
        {
            return repo.GetAllFilters(searchQuery, selectedFilters);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> AddNewBook()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/Uploads");

            var uniqueFolderName = Guid.NewGuid().ToString();
            var uniqueFolderPath = Path.Combine(root, uniqueFolderName);

            Directory.CreateDirectory(uniqueFolderPath);

            var provider = new MultipartFormDataStreamProvider(uniqueFolderPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string coverImagePath = null, filePath = null;
                Book newBook = new Book
                {
                    Title = provider.FormData["title"],
                    Language = provider.FormData["language"],
                    PageCount = Convert.ToInt32(provider.FormData["numOfPages"]),
                    Description = provider.FormData["description"]
                };

                if (int.TryParse(provider.FormData["author"], out int authorId))
                    newBook.AuthorId = authorId;

                if (int.TryParse(provider.FormData["category"], out int categoryId))
                    newBook.CategoryId = categoryId;

                var genresJson = provider.FormData["genres"];
                if (!string.IsNullOrEmpty(genresJson))
                {
                    var genres = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(genresJson);
                    newBook.Genres = genres.Select(genreId => new Genre { Id = genreId }).ToList();
                }

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.Name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var originalFileName = Path.GetFileName(file.Headers.ContentDisposition.FileName.Trim('"'));

                    var destinationPath = Path.Combine(uniqueFolderPath, originalFileName);
                    File.Move(localFileName, destinationPath);

                    var relativePath = Path.Combine("Uploads", uniqueFolderName, originalFileName).Replace("\\", "/");

                    if (name == "pdf")
                        newBook.FilePath = relativePath;
                    else if (name == "file")
                        newBook.CoverImagePath = relativePath;
                }

                return Ok(await repo.AddNewBook(newBook));
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var book = await repo.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IHttpActionResult> UpdateBook(long id)
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/Uploads");
            var uniqueFolderName = Guid.NewGuid().ToString();
            var uniqueFolderPath = Path.Combine(root, uniqueFolderName);

            Directory.CreateDirectory(uniqueFolderPath);

            var provider = new MultipartFormDataStreamProvider(uniqueFolderPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                // Find the existing book
                var existingBook = await repo.GetBookById(id);
                if (existingBook == null)
                    return NotFound();

                // Remove the old folder and files if they exist
                //if (!string.IsNullOrEmpty(existingBook.FilePath))
                //{
                //    var oldFolderPath = HttpContext.Current.Server.MapPath("~/" + Path.GetDirectoryName(existingBook.FilePath));
                //    if (Directory.Exists(oldFolderPath))
                //    {
                //        Directory.Delete(oldFolderPath, true); // true to delete recursively
                //    }
                //}

                // Update the book's properties
                existingBook.Title = provider.FormData["title"];
                existingBook.Language = provider.FormData["language"];
                existingBook.PageCount = Convert.ToInt32(provider.FormData["numOfPages"]);
                existingBook.Description = provider.FormData["description"];

                if (int.TryParse(provider.FormData["author"], out int authorId))
                    existingBook.AuthorId = authorId;

                if (int.TryParse(provider.FormData["category"], out int categoryId))
                    existingBook.CategoryId = categoryId;

                var genresJson = provider.FormData["genres"];
                if (!string.IsNullOrEmpty(genresJson))
                {
                    var genres = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(genresJson);
                    existingBook.Genres = genres.Select(genreId => new Genre { Id = genreId }).ToList();
                }

                // Handle file uploads
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.Name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var originalFileName = Path.GetFileName(file.Headers.ContentDisposition.FileName.Trim('"'));

                    var destinationPath = Path.Combine(uniqueFolderPath, originalFileName);
                    File.Move(localFileName, destinationPath);

                    // Формуємо шлях відносно папки Uploads/
                    var relativePath = Path.Combine("Uploads", uniqueFolderName, originalFileName).Replace("\\", "/");

                    if (name == "pdf")
                        existingBook.FilePath = relativePath; // Відносний шлях
                    else if (name == "file")
                        existingBook.CoverImagePath = relativePath; // Відносний шлях
                }

                // Save the updated book
                await repo.UpdateBook(existingBook);

                return Ok("Book updated successfully.");
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("get-authors")]
        public List<Author> GetAllAuthors()
        {
            return repo.GetAllAuthors();
        }

        [HttpGet]
        [Route("get-categories")]
        public List<Category> GetAllCategories()
        {
            return repo.GetAllCategories();
        }

        [HttpGet]
        [Route("get-genres")]
        public List<Genre> GetAllGenres()
        {
            return repo.GetAllGenres();
        }
    }
}
