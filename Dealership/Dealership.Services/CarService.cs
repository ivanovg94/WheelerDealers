﻿using Dealership.Data.CompositeModels;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private readonly DealershipContext context;

        public CarService(DealershipContext context)
        {
            this.context = context;
        }

        public Car CreateCar(int brandId, int carModelId, int mileage, short horsePower,
            short engineCapacity, DateTime productionDate, decimal price, int bodyTypeId,
            string colorName, int colorTypeId, int fuelTypeId, int gearBoxTypeId, byte numberOfGears)
        {

            var color = this.context.Colors
                                                    .Include(c => c.ColorType)
                                                    .FirstOrDefault(c => c.Name == colorName
                                                     && c.ColorTypeId == colorTypeId);

            var colorTypeFromDatabase = this.context.ColorTypes
                                       .FirstOrDefault(ct => ct.Id == colorTypeId);

            if (color == null)
            {
                color = new Color { Name = colorName, ColorType = colorTypeFromDatabase };
                this.context.Colors.Add(color);
                this.context.SaveChanges();
            }

            var fuelType = this.context.FuelTypes
                                          .FirstOrDefault(f => f.Id == fuelTypeId);


            var gearbox = this.context.Gearboxes
                .FirstOrDefault(g => g.GearType.Id == gearBoxTypeId
                                  && g.NumberOfGears == numberOfGears);

            if (mileage < 0)
            {
                throw new ServiceException($"Mileage must be greater than 0.");
            }
            var newCar = new Car()
            {
                BrandId = brandId,
                CarModelId = carModelId,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Mileage = mileage,
                BodyTypeId = bodyTypeId,
                Color = color,
                ColorId = color.Id,
                FuelTypeId = fuelTypeId,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }


        public Car AddCar(Car car)
        {
            if (car == null)
            {
                throw new ServiceException("Car doesn't exist!");
            }
            car = this.context.Cars.Add(car).Entity;
            this.context.SaveChanges();

            return car;
        }

        public void AddCars(ICollection<Car> cars)
        {
            foreach (var car in cars)
            {
                if (car != null)

                {
                    this.context.Cars.Add(car);
                }
            }
            this.context.SaveChanges();
        }

        //public IList<Car> GetCars(int brandId, int modelId, int skip, int take)
        //{
        //    var querry = this.context.Cars
        //                                    .Skip(skip)
        //                                    .Take(take)
        //                                    .Include(c => c.Brand)
        //                                    .Include(c => c.CarsExtras)
        //                                         .ThenInclude(ce => ce.Extra)
        //                                    .Include(c => c.BodyType)
        //                                    .Include(c => c.Color)
        //                                        .ThenInclude(co => co.ColorType)
        //                                    .Include(c => c.FuelType)
        //                                    .Include(c => c.GearBox)
        //                                        .ThenInclude(gb => gb.GearType)
        //                                    .Include(c => c.Images);

        //    return querry.ToList();
        //}

        public virtual Car GetCar(int id)
        {
            var car = this.context.Cars
                                  .Where(c => c.Id == id)
                                  .Include(c => c.Brand)
                                  .Include(c => c.CarModel)
                                  .Include(c => c.CarsExtras)
                                       .ThenInclude(ce => ce.Extra)
                                  .Include(c => c.BodyType)
                                  .Include(c => c.Color)
                                      .ThenInclude(co => co.ColorType)
                                  .Include(c => c.FuelType)
                                  .Include(c => c.GearBox)
                                      .ThenInclude(gb => gb.GearType)
                                  .Include(c => c.Images)
                                  .FirstOrDefault();

            if (car == null)
            {
                throw new ServiceException($"There is no car with ID {id}.");
            }
            return car;
        }

        public Car RemoveCar(int id)
        {
            var car = GetCar(id);
            this.context.Cars.Remove(car);

            var usersCars = this.context.UsersCars.Where(uc => uc.CarId == id).ToList();

            foreach (var userCar in usersCars)
            {
                this.context.UsersCars.Remove(userCar);
            }

            var carsExtras = this.context.CarsExtras.Where(uc => uc.CarId == id).ToList();

            foreach (var carExtra in carsExtras)
            {
                this.context.CarsExtras.Remove(carExtra);
            }

            this.context.SaveChanges();
            return car;
        }

        public int GetAllCarsCount()
        {
            return this.context.Cars.Count();
        }

        public int GetCountWithCriteria(Expression<Func<Car, bool>> filterCriteria)
        {
            return this.context.Cars.Where(filterCriteria).Count();
        }

        public void Update(Car car)
        {
            this.context.Cars.Update(car);
            this.context.SaveChanges();
        }

        public void SaveImages(string root, IList<string> fileNames, IList<Stream> stream, int carId)
        {
            var car = GetCar(carId);

            if (car == null)
            {
                throw new InvalidOperationException("Car not found");
            }

            for (int i = 0; i < fileNames.Count; i++)
            {
                var fileName = fileNames[i];
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                var path = Path.Combine(root, imageName);

                using (var fileStream = File.Create(path))
                {
                    stream[i].CopyTo(fileStream);
                }

                var image = new Image()
                {
                    ImageName = imageName
                };

                car.Images.Add(image);
            }

            this.context.SaveChanges();
        }

        public CarSearchResult GetCarSearchResult(int brandId, int modelId, int sortKey, int page = 0)
        {
            var take = 5;
            var skip = page * take;

            //TODO: INCLUDABLE?
            IQueryable<Car> totalCars = null;

            if (brandId == 0)
            {
                totalCars = this.context.Cars.Where(c => c.IsSold == false
                                                      && c.IsDeleted == false);
            }
            else if (brandId != 0 && modelId == 0)
            {
                totalCars = this.context.Cars.Where(c => c.BrandId == brandId
                                                      && c.IsSold == false
                                                      && c.IsDeleted == false);
            }
            else if (brandId != 0 && modelId != 0)
            {
                totalCars = this.context.Cars.Where(c => c.BrandId == brandId
                                                      && c.CarModelId == modelId
                                                      && c.IsSold == false
                                                      && c.IsDeleted == false);
            }

            var totalCount = totalCars.Count();

            if (sortKey == 1) { totalCars.OrderBy(c => c.Price); }
            else if (sortKey == 2) { totalCars.OrderByDescending(c => c.Price); }

            var cars = totalCars.Skip(skip).Take(take)
                                           .Include(c => c.Brand)
                                           .Include(c => c.CarModel)
                                           .Include(c => c.GearBox)
                                               .ThenInclude(gb => gb.GearType)
                                           .Include(c => c.Color)
                                               .ThenInclude(c => c.ColorType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.Images)
                                            .Select(c => new CarSummary(c))
                                            .ToList();

            var result = new CarSearchResult
            {
                FoundCars = cars,
                TotalCount = totalCount
            };

            return result;
        }



        public IList<Car> GetCars(int skip, int take, int sort)
        {

            var querry = this.context.Cars
                                          .Skip(skip)
                                          .Take(take)
                                          .Include(c => c.Brand)
                                          .Include(c => c.CarsExtras)
                                               .ThenInclude(ce => ce.Extra)
                                          .Include(c => c.BodyType)
                                          .Include(c => c.Color)
                                              .ThenInclude(co => co.ColorType)
                                          .Include(c => c.FuelType)
                                          .Include(c => c.GearBox)
                                              .ThenInclude(gb => gb.GearType)
                                          .Include(c => c.Images);

            return SortByPrice(sort, querry);
        }

        public IList<Car> GetCars(int skip, int take, Expression<Func<Car, bool>> filterCriteria, int sort)
        {
            var querry = this.context.Cars
                                            .Where(filterCriteria)
                                            .Skip(skip)
                                            .Take(take)
                                            .Include(c => c.Brand)
                                            .Include(c => c.CarsExtras)
                                                 .ThenInclude(ce => ce.Extra)
                                            .Include(c => c.BodyType)
                                            .Include(c => c.Color)
                                                .ThenInclude(co => co.ColorType)
                                            .Include(c => c.FuelType)
                                            .Include(c => c.GearBox)
                                                .ThenInclude(gb => gb.GearType)
                                            .Include(c => c.Images);


            return SortByPrice(sort, querry);
        }

        private static IList<Car> SortByPrice(int sort, IIncludableQueryable<Car, ICollection<Image>> querry)
        {
            return sort == -1
                ? querry.ToList()
                : sort == 1
                      ? querry.OrderBy(c => c.Price).ToList()
                      : querry.OrderByDescending(c => c.Price).ToList();
        }
    }
}

