using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Data;
using Bookify.Data.Models;
using Bookify.Domain.DTOs;

namespace Bookify.Domain.Repositories
{
    public class AuthorsRepository
    {
        private readonly AuthContext _context;

        public AuthorsRepository()
        {
            _context = new AuthContext();
        }

        public List<AuthorDTO> GetAll()
        {
            var authors = _context.Authors.ToList();

            return authors.Select(author => AuthorDTO.FromData(author)).ToList();
        }

        public AuthorDTO GetById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return null;
            }

            return AuthorDTO.FromData(author);
        }

        public async Task<int> AddAsync(Author author)
        {
            var createdAuthor = _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return createdAuthor.Id;
        }

        public void Update(AuthorDTO author, int authorId)
        {
            var existingAuthor = _context.Authors.Find(authorId);
            if (existingAuthor != null)
            {
                existingAuthor.FirstName = author.FirstName;
                existingAuthor.LastName = author.LastName;
                existingAuthor.Biography = author.Biography;
                existingAuthor.BirthDate = author.BirthDate;
                existingAuthor.DeathDate = author.DeathDate;
                existingAuthor.Country = author.Country;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }
    }
}
