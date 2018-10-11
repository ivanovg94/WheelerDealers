using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        User RegisterUser(string username, string password, string confirmPassword, string email);

        //User GetUser(string username);

        //bool IsUserExisting(string username);

        //bool IsEmailExisting(string email);

        User Login(string username, string password);

        User GetCurrentUser();

        void Logout();
    }
}
