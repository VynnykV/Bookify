using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Api.Models;
using Bookify.Domain.Repositories;

namespace Bookify.Api.Controllers
{
    [RoutePrefix("api/Authors")]
    public class AuthorsController : ApiController
    {
        private readonly AuthorsRepository _repo;

        public AuthorsController()
        {
            _repo = new AuthorsRepository();
        }

        // GET api/Authors
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var authors = _repo.GetAll();

            return Ok(authors);
        }

        // GET api/Authors/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var author = _repo.GetById(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST api/Authors
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] CreateAuthorModel createAuthorModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _repo.AddAsync(CreateAuthorModel.ToData(createAuthorModel)));
        }

        // PUT api/Authors/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] CreateAuthorModel сreateAuthorModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.Update(CreateAuthorModel.FromData(сreateAuthorModel), id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/Authors/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var existingAuthor = _repo.GetById(id);
            if (existingAuthor == null)
                return NotFound();

            _repo.Delete(id);

            return Ok();
        }
    }
}
