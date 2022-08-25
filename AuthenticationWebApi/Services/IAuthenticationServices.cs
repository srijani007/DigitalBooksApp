using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace AuthenticationWebApi.Services
{
    public interface IAuthenticationServices
    {
        LibraryContext libDbContext { get; set; }

        string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName,string userrole);
        List<DigitalBooksUser> validateuser(Creds usersdetails);
    }
}