using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace BookApi.Services
{
    public interface IBookServices
    {
        LibraryContext libDbContext { get; set; }

        string AddBook(BookDetails book);
        List<Book> GetBookbyAuthordetails(GetBookbyAuthorId getbook);
        List<Book> GetBooks();
        List<Book> GetContent(Viewcontent viewcontent);
       // int RemoveBook(Viewcontent book);
        List<Book> SearchBookbyConditions(SearchBooks searchbooks);
        string Updatebooks(Updatebookdetails book);
    }
}