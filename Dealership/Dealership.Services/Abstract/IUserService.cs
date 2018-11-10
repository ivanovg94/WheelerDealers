using Dealership.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        ICollection<User> GetUsers();

        Task<Car> AddCarToFavorites(int carId, User user);

        Task<Car> RemoveCarFromFavorites(int carId, User user);

        Task<IList<Car>> GetFavorites(User user);

        bool IsCarFavorite(int carId, User user);
    }
}
