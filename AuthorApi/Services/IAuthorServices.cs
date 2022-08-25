using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using System.Security.Claims;

namespace AuthorApi.Services
{
    public interface IAuthorServices
    {
        LibraryContext libDbContext { get; set; }

        List<UserClaims> AuthorizeUser(ClaimsIdentity claimsIdentity);
        List<DigitalBooksUser> GetUsers();
        string RemoveUser(DigitalBooksUser userDetails);
        string UpdateUserAccount(DigitalBooksUser userDetails);
        List<DigitalBooksUser> UsersignUp(AuthorDetails authorDetails);
    }
}