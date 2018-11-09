using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {

        Car CreateCar(int brandId, int carModelId, int mileage, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, int bodyTypeId, string colorName, int colorTypeId,
            int fuelTypeId, int gearBoxTypeId, byte numberOfGears, ICollection<int> extrasIds);

        void AddCars(ICollection<Car> cars);

        Task<Car> GetCar(int id);

        Task<IList<Car>> GetCarsAsync();

        Task<IList<Car>> GetCarsAsync(int skip, int take);

        Task<Car> RemoveCar(int carId);

        int GetCarsCount();

        void Update(Car car);

        void SaveImages(string root, IList<string> fileNames, IList<Stream> stream, int carId);

    }
}