using System.Collections.Generic;
using System.Web.Http;
using Bookify.Domain.Repositories;
using Bookify.Domain.DTOs;

namespace Bookify.Api.Controllers
{
    [RoutePrefix("api/libraries")]
    public class LibrariesController : AuthorizationController
    {
        private LibrariesRepository repo = null;

        public LibrariesController()
        {
            repo = new LibrariesRepository();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("get-book-list")]
        public List<AdminBookListDTO> GetBookList(string sortOption, bool descending, string searchQuery, int pageNumber)
        {
            return repo.GetBookList(sortOption, descending, searchQuery, pageNumber);
        }
    }
}
