using System;
using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services
{
    public interface ICarService
    {
        Car AddCar(Brand brand, string model, short horsePower, short engineCapacity
            , DateTime productionDate, decimal price, Chassis chassis
            , Color color, FuelType fuelType, Gearbox gearbox);

        Car GetCar();

        IEnumerable<Car> GetCars(bool filterSold, string direction);
    }
}