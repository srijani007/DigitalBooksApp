using CommonSpace.ApplicationVariables;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace BookApi.Services
{
    public class BookServices : IBookServices
    {
        public LibraryContext libDbContext { get; set; }

        public BookServices(LibraryContext dbcontext)
        {
            libDbContext = dbcontext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Book added successfully</returns>
        /// <exception cref="Exception"></exception>
        public List<Book> AddBook(BookDetails book)
        {
            try
            {
                Book bookDetails = new Book();
                bookDetails.Title = book.Title;
                bookDetails.Content = book.Content;
                bookDetails.AuthorName = book.AuthorName;
                bookDetails.Publisher = book.Publisher;
                bookDetails.CreatedDate = DateTime.UtcNow;
                bookDetails.Active = book.Active;
                bookDetails.Category = book.Category;
                bookDetails.ModifiedDate = DateTime.UtcNow;
                bookDetails.Logo = book.Logo;
                bookDetails.Price = book.Price;
                bookDetails.PublishedDate = DateTime.UtcNow;
                libDbContext.Books.Add(bookDetails);
                libDbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Book status updated</returns>
        /// <exception cref="Exception"></exception>
        public string UpdateBookDetails(BookBlock book)
        {
            try
            {
                var updatebook = libDbContext.Books.Where(x => x.BookId == book.BookId)
                .FirstOrDefault();
                if (updatebook == null)
                {
                    return AppVariables.bookNotFound;
                }
                else
                {
                    updatebook.Active = book.Active;
                    libDbContext.Books.Update(updatebook);
                    libDbContext.SaveChanges();
                    return AppVariables.bookstatusupdated;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>To see\get all books</returns>
        /// <exception cref="Exception"></exception>

        public List<Book> GetBooks()
        {
            try
            {
                return libDbContext.Books.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Book> GetBookbyAuthordetails(GetBookbyAuthorId getbook)
        {
            try
            {
                var bookdetails = libDbContext.Books.Where(x => x.AuthorName == getbook.UserName).Select(x => x);
                if (bookdetails != null)
                {
                    return bookdetails.ToList();
                }
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Searching books by the criteria</returns>
        /// <exception cref="Exception"></exception>
        public List<Book> SearchBookbyConditions(SearchBooks searchbooks)
        {
            try
            {
                if (searchbooks.Category != null || searchbooks.Price >= 0 || searchbooks.AuthorName != null)
                {
                    var result = libDbContext.Books.Where(b => b.Category == searchbooks.Category || b.AuthorName == searchbooks.AuthorName || b.Price == searchbooks.Price)
                       .Select(b => b).ToList();
                    return result;
                }
                else if (searchbooks.Category != null || searchbooks.Price >= 0)
                {
                    var result = libDbContext.Books.Where(b => b.Category == searchbooks.Category || b.Price == searchbooks.Price)
                        .Select(b => b).ToList();
                    return result;
                }
                else if (searchbooks.Price >= 0 || searchbooks.AuthorName != null)
                {
                    var result = libDbContext.Books.Where(b => b.AuthorName == searchbooks.AuthorName || b.Price == searchbooks.Price)
                        .Select(b => b).ToList();
                    return result;
                }
                else if (searchbooks.Category != null || searchbooks.AuthorName != null)
                {
                    var result = libDbContext.Books.Where(b => b.Category == searchbooks.Category || b.AuthorName == searchbooks.AuthorName)
                    .Select(b => b).ToList();
                    return result;
                }
                else if (searchbooks.Category != null)
                {
                    var result = libDbContext.Books.Where(b => b.Category == searchbooks.Category).Select(b => b).ToList();
                    return result;
                }
                else if (searchbooks.AuthorName != null)
                {
                    var result = libDbContext.Books.Where(b => b.AuthorName == searchbooks.AuthorName).Select(b => b).ToList();
                    return result;
                }
                else
                {
                    var result = libDbContext.Books.Where(b => b.Price == searchbooks.Price).Select(b => b).ToList();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
