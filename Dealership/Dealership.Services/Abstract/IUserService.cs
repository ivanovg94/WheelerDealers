using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        User RegisterUser(string username, string password, string confirmPassword, string email);

        User GetUserByCredentials(string username, string password);

        User DeleteUser(string username, string password);
    }
}
