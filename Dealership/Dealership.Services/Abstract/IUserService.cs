using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        User RegisterUser(string username, string password, string confirmPassword, string email);

        User GetUserByCredentials(string username, string password);

        User DeleteUser(string username, string password);

        Car AddCarToFavorites(int carId, string username);

        Car RemoveCarFromFavorites(int carId, string username);

        IList<Car> ListFavorites(string username);
    }
}
