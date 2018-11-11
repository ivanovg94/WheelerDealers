using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        Car AddCarToFavorites(int carId, User user);

        IList<Car> GetFavorites(User user);

        ICollection<User> GetUsers();

        bool IsCarFavorite(int carId, User user);

        Car RemoveCarFromFavorites(int carId, User user);
    }
}