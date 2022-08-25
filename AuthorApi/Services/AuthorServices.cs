using CommonSpace.ApplicationVariables;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using System.Security.Claims;

namespace AuthorApi.Services
{
    public class AuthorServices : IAuthorServices
    {
        public LibraryContext libDbContext { get; set; }

        public AuthorServices(LibraryContext libraryContext)
        {
            libDbContext = libraryContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the users from the database</returns>
        /// <exception cref="Exception"></exception>
        public List<DigitalBooksUser> GetUsers()
        {
            try
            {
                return libDbContext.DigitalBooksUsers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorDetails"></param>
        /// <returns>author deatils get added\inserted in database</returns>
        public List<DigitalBooksUser> UsersignUp(AuthorDetails authorDetails)
        {
            try
            {
                var mailid = libDbContext.DigitalBooksUsers.Where(x => x.UserName == authorDetails.Email).Select(x => x.Email).FirstOrDefault();
                if (mailid == authorDetails.Email)
                {
                    return null;
                }
                else
                {
                    DigitalBooksUser digitalBooksUser = new DigitalBooksUser();
                    digitalBooksUser.UserName = authorDetails.UserName;
                    digitalBooksUser.UserPass = authorDetails.UserPass;
                    digitalBooksUser.Email = authorDetails.Email;
                    digitalBooksUser.UserRole = authorDetails.UserRole;
                    libDbContext.DigitalBooksUsers.Add(digitalBooksUser);
                    libDbContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Records has been updated</returns>
        /// <exception cref="Exception"></exception>

        public string UpdateUserAccount(DigitalBooksUser userDetails)
        {
            try
            {
                //if user needs to update his records
                var lst = libDbContext.DigitalBooksUsers.ToList();
                int index = lst.FindIndex(s => s.UserId == userDetails.UserId);
                lst[index].UserName = userDetails.UserName;
                lst[index].Email = userDetails.Email;
                lst[index].UserPass = userDetails.UserPass;
                lst[index].UserRole = userDetails.UserRole;
                libDbContext.DigitalBooksUsers.Update(lst[index]);
                libDbContext.SaveChanges();

                return AppVariables.recordsUpdate;
            }
            catch (IndexOutOfRangeException ex)
            {
                return $"{AppVariables.userIdNotFound},{ex.Message}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns> user has been deleted or removed</returns>
        /// <exception cref="Exception"></exception>
        public string RemoveUser(DigitalBooksUser userDetails)
        {
            try
            {
                DigitalBooksUser? digitalBooksUser = new DigitalBooksUser();
                digitalBooksUser = libDbContext.DigitalBooksUsers.Where(s => s.UserId == userDetails.UserId).FirstOrDefault();
                if (digitalBooksUser != null)
                {
                    libDbContext.DigitalBooksUsers.Remove(digitalBooksUser);
                    return AppVariables.userRemoved;
                }
                return AppVariables.userDoesnotExists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserClaims> AuthorizeUser(ClaimsIdentity claimsIdentity)
        {
            List<UserClaims> claims = (List<UserClaims>)claimsIdentity.Claims;
            var list = new List<UserClaims>
            {
            new UserClaims{
            UserRole = claimsIdentity.FindFirst("UserRole").ToString(),
            }
            };
            return list;
        }

    }
}
