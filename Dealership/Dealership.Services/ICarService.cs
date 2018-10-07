using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services
{
    public interface ICarService
    {
        Car AddCar();
        Car GetCar();

        IEnumerable<Car> GetCars(bool filterSold, string direction);
    }
}