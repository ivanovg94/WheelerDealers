//using Dealership.Data.Models;
//using Dealership.Data.UnitOfWork;
//using Dealership.Services.Abstract;
//using Dealership.Services.Exceptions;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace Dealership.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IUnitOfWork unitOfWork;
//        private readonly ICarService carService;

//        public UserService()
//        {

//        }

//        public UserService(IUnitOfWork unitOfWork, ICarService carService)
//        {
//            this.unitOfWork = unitOfWork;
//            this.carService = carService;
//        }

//        public User RegisterUser(string username, string password, string confirmPassword, string email)
//        {
//            if (IsUserExisting(username))
//            {
//                throw new ServiceException("There is already registered user with that username.");
//            }

//            if (IsEmailExisting(email))
//            {
//                throw new ServiceException("There is already registered user with that email.");
//            }

//            if (password != confirmPassword)
//            {
//                throw new ServiceException("Password does not match the confirm password.");
//            }

//            if (password.Length < 3)
//            {
//                throw new ArgumentException("The length of the password cannot be less than 3 symbols");
//            }

//            if (username.Length < 3 || username.Length > 25)
//            {
//                throw new ArgumentException("The length of the username cannot be less than 3 and more than 25 symbols.");
//            }

//            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

//            Match match = regex.Match(email);
//            if (!match.Success)
//            {
//                throw new ArgumentException("Invalid email address.");
//            }

//            var user = new User()
//            {
//                Username = username,
//                Password = password,
//                Email = email,
//                UserType = Enum.Parse<UserType>("User")
//            };

//            this.unitOfWork.GetRepository<User>().Add(user);
//            this.unitOfWork.SaveChanges();

//            return user;
//        }

//        public User GetUserByCredentials(string username, string password)
//        {
//            User user = GetUserByUsername(username);

//            if (user == null || user.Password != password)
//            {
//                throw new ServiceException("Invalid username or password.");
//            }

//            return user;
//        }

//        private User GetUserByUsername(string username)
//        {
//            var user = this.unitOfWork
//                .GetRepository<User>()
//                .All()
//                .FirstOrDefault(u => u.UserName == username);

//            return user;
//        }

//        public User DeleteUser(string username, string password)
//        {
//            var user = GetUserByCredentials(username, password);

//            this.unitOfWork
//                .GetRepository<User>()
//                .Delete(user);
//            this.unitOfWork.SaveChanges();

//            return user;
//        }

//        public Car AddCarToFavorites(int carId, string username)
//        {
//            Car car = this.carService.GetCar(carId);
//            User user = GetUserByUsername(username);

//            var isCarExists = this.unitOfWork.GetRepository<UsersCars>().All().Any(uc => uc.CarId == car.Id && uc.UserId == user.Id);

//            if (isCarExists)
//            {
//                throw new ServiceException("This car is already added to favorites.");
//            }

//            var isUserCarDeleted = this.unitOfWork.GetRepository<UsersCars>().AllAndDeleted().FirstOrDefault(uc => uc.CarId == carId && uc.User == user);

//            if (isUserCarDeleted == null)
//            {
//                var newUserCar = new UsersCars() { CarId = carId, User = user };
//                this.unitOfWork.GetRepository<UsersCars>().Add(newUserCar);
//                this.unitOfWork.SaveChanges();
//            }
//            else
//            {
//                isUserCarDeleted.IsDeleted = false;
//                this.unitOfWork.SaveChanges();
//            }

//            return car;
//        }

//        public Car RemoveCarFromFavorites(int carId, string username)
//        {
//            Car car = this.carService.GetCar(carId);
            
//            var usersCars = this.unitOfWork.GetRepository<UsersCars>().All().FirstOrDefault(uc => uc.CarId == carId && uc.User.Username == username);

//            if (usersCars == null)
//            {
//                throw new ServiceException("This car is not added to favorites.");
//            }

//            this.unitOfWork.GetRepository<UsersCars>().Delete(usersCars);
//            this.unitOfWork.SaveChanges();

//            return car;
//        }

//        public IList<Car> ListFavorites(string username)
//        {
//            var userCars = this.unitOfWork.GetRepository<User>().All()
//                                        .Include(u => u.UsersCars)
//                                        .ThenInclude(uc => uc.Car)
//                                        .FirstOrDefault(u => u.Username == username)
//                                        .UsersCars;

//            var cars = new List<Car>();

//            foreach (var uc in userCars)
//            {
//                if (uc.IsDeleted == false)
//                {
//                    var car = this.carService.GetCar(uc.CarId);
//                    cars.Add(car);
//                }
//            }

//            return cars;
//        }

//        private bool IsUserExisting(string username)
//        {
//            return this.unitOfWork
//                    .GetRepository<User>()
//                    .All()
//                    .Any(u => u.Username == username);
//        }

//        private bool IsEmailExisting(string email)
//        {
//            return this.unitOfWork
//                    .GetRepository<User>()
//                    .All()
//                    .Any(u => u.Email == email);
//        }
//    }
//}
