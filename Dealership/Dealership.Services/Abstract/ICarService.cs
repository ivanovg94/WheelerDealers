using Dealership.Data.Models;
using System;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {
        Car CreateCar(string brandName, string model, short horsePower, short engineCapacity
           , DateTime productionDate, decimal price, string chassisName, string colorName, string colorType, string fuelTypeName, string gearboxTypeName, int numOfGears);

        void AddCar(Car car);

        void AddCars(ICollection<Car> cars);

        Car GetCar(int id);

        IList<Car> GetCars(bool filterSold, string direction);

        IList<Car> GetCars(string direction);

        Brand GetBrand(string brandName);

        Car RemoveCar(int carId);
    }
}