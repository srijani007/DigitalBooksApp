using BookApi.Services;
using CommonSpace.ApplicationVariables;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices BookServices)
        {
            _bookServices = BookServices;

        }

        [HttpGet("GetBooks")]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                var result = _bookServices.GetBooks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("AddBooks")]
        [Authorize]
        public ActionResult<List<Book>> AddBookDetails([FromBody] BookDetails book)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var roletype = identity.FindFirst("UserRole").Value;
                    if (roletype == "author" || roletype == "Author")
                    {
                        var result = _bookServices.AddBook(book);
                        if (book == null)
                        {
                            BadRequest();
                        }
                        else
                        {
                            return result;
                        }
                        return Ok();
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateBooks")]
        public ActionResult<string> EditBookDeatils([FromBody] BookBlock book)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var roletype = identity.FindFirst("RoleId").ToString();
                    if (roletype == "Author")
                    {
                        string result = _bookServices.UpdateBookDetails(book);

                        if (book == null)
                        {
                            BadRequest();
                        }
                        else
                        {
                            return result;
                        }
                        return Ok("");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("GetBookbyName")]
        public ActionResult<string> GetBookByname([FromBody] GetBookbyAuthorId getbook)
        {
            try
            {
                var books = _bookServices.GetBookbyAuthordetails(getbook);
                if(books == null)
                {
                    return AppVariables.bookNotFound;
                }
                else
                {
                    return Ok(books);
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
