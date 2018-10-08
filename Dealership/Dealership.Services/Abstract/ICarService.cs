using Dealership.Data.Models;
using System;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {
        Car CreateCar(Brand brand, string model, short horsePower, short engineCapacity
            , DateTime productionDate, decimal price, Chassis chassis
            , Color color, FuelType fuelType, Gearbox gearbox);

        Car AddCar(Brand brand, string model, short horsePower, short engineCapacity
            , DateTime productionDate, decimal price, Chassis chassis
            , Color color, FuelType fuelType, Gearbox gearbox);

        Car GetCar(int id);

        IEnumerable<Car> GetCars(bool filterSold, string direction);
    }
}