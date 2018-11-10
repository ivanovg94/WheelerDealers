using Dealership.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        ICollection<User> GetUsers();

        Task<Car> AddCarToFavoritesAsync(int carId, User user);

        Task<Car> RemoveCarFromFavoritesAsync(int carId, User user);

        Task<IList<Car>> GetFavoritesAsync(User user);

        bool IsCarFavorite(int carId, User user);
    }
}
