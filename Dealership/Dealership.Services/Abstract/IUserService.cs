using Dealership.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        ICollection<User> GetUsers();

        Car AddCarToFavorites(int carId, User user);

        Car RemoveCarFromFavorites(int carId, User user);

        IList<Car> GetFavorites(User user);

        bool IsCarFavorite(int carId, User user);
    }
}
