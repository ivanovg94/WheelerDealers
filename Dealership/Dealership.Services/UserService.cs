﻿using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class UserService : IUserService
    {
        private readonly ICarService carService;
        private readonly DealershipContext dealershipContext;

        public UserService(ICarService carService, DealershipContext dealershipContext)
        {
            this.carService = carService;
            this.dealershipContext = dealershipContext;
        }

        public ICollection<User> GetUsers()
        {
            var users = this.dealershipContext.Users.Include(u => u.UsersCars)
                                                    .ToList();
            return users;
        }

        public Car AddCarToFavorites(int carId, User user)
        {
            Car car = this.carService.GetCar(carId);

            var isCarFavorite = IsCarFavorite(carId, user);

            if (isCarFavorite)
            {
                throw new ServiceException("This car is already added to favorites.");
            }

            var newUserCar = new UsersCars() { CarId = carId, User = user };
            this.dealershipContext.UsersCars.Add(newUserCar);
            this.dealershipContext.SaveChanges();
            return car;
        }

        public Car RemoveCarFromFavorites(int carId, User user)
        {
            Car car = this.carService.GetCar(carId);

            var usersCars = this.dealershipContext.UsersCars.FirstOrDefault(uc => uc.CarId == carId && uc.User == user);

            if (usersCars == null)
            {
                throw new ServiceException("This car is not added to favorites.");
            }

            this.dealershipContext.UsersCars.Remove(usersCars);
            this.dealershipContext.SaveChanges();
            return car;
        }

        public IList<Car> GetFavorites(User user)
        {
            var userCars = this.dealershipContext.Users
                                        .Include(u => u.UsersCars)
                                        .ThenInclude(uc => uc.Car)
                                        .FirstOrDefault(u => u == user)
                                        .UsersCars;

            var cars = new List<Car>();
            foreach (var uc in userCars)
            {
                if (uc.IsDeleted == false)
                {
                    var car = this.carService.GetCar(uc.CarId);
                    cars.Add(car);
                }
            }

            return cars;
        }

        public bool IsCarFavorite(int carId, User user)
        {
            return this.dealershipContext.UsersCars.Any(uc => uc.CarId == carId && uc.UserId == user.Id);
        }
    }
}
