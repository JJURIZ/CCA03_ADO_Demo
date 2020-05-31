using System.Collections.Generic;

namespace AuthorManager.Models
{
    public interface IAuthorRepository
    {
        void AddAuthor(Author newAuthor);
        Author GetById(int Id);
        List<Author> ListAll();
    }
}