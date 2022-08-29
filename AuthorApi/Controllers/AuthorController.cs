using AuthorApi.Services;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //  [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorServices _authorServices;
        public AuthorController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        [HttpGet("GetUser")]

        public ActionResult<List<DigitalBooksUser>> GetAllUsers()
        {
            try
            {
             return Ok("users displyed");                
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult<string> UpdateUserDetails([FromBody] DigitalBooksUser user)
        {
            try
            {
                string result = _authorServices.UpdateUserAccount(user);
                return Ok(new { result } );
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteUser")]
        public ActionResult<string> DeleteUserDetails([FromBody] DigitalBooksUser user)
        {
            try
            {
                string result = _authorServices.RemoveUser(user);
                return Ok(  result );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public ActionResult<List<DigitalBooksUser>> Signing_up([FromBody] AuthorDetails user)
        {
            try
            {
                var result = _authorServices.UsersignUp(user);
                return Ok(result);
            }
            catch
            {
                return null;
            }
        }
    }
}
