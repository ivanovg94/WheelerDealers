﻿using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {
        Car CreateCar(string brandName, string model, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, string chassisName, string colorName,
            string colorType, string fuelTypeName, string gearboxTypeName, int numOfGears);

        Car AddCar(Car car);

        void AddCars(ICollection<Car> cars);

        Car GetCar(int id);

        IList<Car> GetCars(bool filterSold, string direction);

        IList<Car> GetCars(string direction);

        IList<Car> GetCars(int skip, int take);

        Car RemoveCar(int carId);

        int GetCarsCount();

        void Update(ICar car);

        void SaveAvatarImage(string root, string filename, Stream stream, int carId);
    }
}