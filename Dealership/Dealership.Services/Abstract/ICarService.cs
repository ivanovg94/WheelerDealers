using Dealership.Data.CompositeModels;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {

        Car CreateCar(int brandId, int carModelId, int mileage, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, int bodyTypeId, string colorName, int colorTypeId,
            int fuelTypeId, int gearBoxTypeId, byte numberOfGears);

        Car AddCar(Car car);

        void AddCars(ICollection<Car> cars);

        Car GetCar(int id);

        Car RemoveCar(int carId);

        CarSearchResult GetCarSearchResult(int brandId, int modelId, int sortKey, int page);

        int GetAllCarsCount();

        int GetCountWithCriteria(Expression<Func<Car, bool>> filterCriteria);

        void Update(Car car);

        void SaveImages(string root, IList<string> fileNames, IList<Stream> stream, int carId);

        IList<Car> GetCars(int skip, int take, int sort);

        IList<Car> GetCars(int skip, int take, Expression<Func<Car, bool>> filterCriteria, int sort);
    }
}