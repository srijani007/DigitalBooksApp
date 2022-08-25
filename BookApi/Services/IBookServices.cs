using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace BookApi.Services
{
    public interface IBookServices
    {
        LibraryContext libDbContext { get; set; }

        List<Book> AddBook(BookDetails book);
        List<Book> GetBookbyAuthordetails(GetBookbyAuthorId getbook);
        List<Book> GetBooks();
        List<Book> SearchBookbyConditions(SearchBooks searchbooks);
        string UpdateBookDetails(BookBlock book);
    }
}