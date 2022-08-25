using AuthenticationWebApi.Services;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IConfiguration _configuration;
        //  private readonly IAuthorServices _authorServices;
        public AuthenticationController(IConfiguration configuration, IAuthenticationServices authenticationServices)//, IAuthorServices authorServices)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticationServices = authenticationServices;//?? throw new ArgumentNullException(nameof(authenticationServices));
                                                             //  _authorServices = authorServices;
        }

        private class RequestedUserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserRole { get; set; }
            public string Email { get; set; }
            public string UserPass { get; set; }
            public RequestedUserInfo(int userId, string userName, string userrole, string email, string userpass)
            {
                UserId = userId;
                UserName = userName;
                UserRole = userrole;
                Email = email;
                UserPass = userpass;
            }
        }

        [HttpPost("SignIn")]
        public ActionResult<string> Validate(Creds usercreds)
        {
            try
            {
                var userdata = ValidateUserCrendentials(usercreds);

                if (userdata != null)
                {
                    var Token = _authenticationServices.BuildToken(_configuration["Jwt:Key"],
                                                    _configuration["Jwt:Issuer"],
                                                    new[]
                                                    {
                                               //   _configuration["Jwt:AudienceGateway"],
                                                   _configuration["Jwt:Audienceauthor"],
                                                    _configuration["Jwt:Audiencereader"],
                                                     _configuration["Jwt:Audiencebook"]
                                                    },
                                                    userdata.UserName,
                                                    userdata.UserRole
                                                    );
                    return Ok(new
                    {
                        Token = Token,
                        IsAuthenticated = true,
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        private RequestedUserInfo ValidateUserCrendentials(Creds userLogin)
        {
            try
            {
                List<DigitalBooksUser> list = _authenticationServices.validateuser(userLogin);
                foreach (var item in list)
                {
                    //Userid = item.User_Id
                    return new RequestedUserInfo(item.UserId, item.UserName, item.UserRole, item.Email, item.UserPass);

                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
