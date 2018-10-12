using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using System;
using System.Linq;

namespace Dealership.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User RegisterUser(string username, string password, string confirmPassword, string email)
        {
            if (IsUserExisting(username))
            {
                throw new InvalidOperationException("There is already registered user with that username.");
            }

            if (IsEmailExisting(email))
            {
                throw new InvalidOperationException("There is already registered user with that email.");
            }

            if (password != confirmPassword)
            {
                throw new InvalidOperationException("Password does not match the confirm password.");
            }

            if (password.Length < 3)
            {
                throw new ArgumentException("The length of the password cannot be less than 3 symbols");
            }

            var user = new User()
            {
                Username = username,
                Password = password,
                Email = email,
            };


            this.unitOfWork.GetRepository<User>().Add(user);
            this.unitOfWork.SaveChanges();

            return user;
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = this.unitOfWork
                .GetRepository<User>()
                .All()
                .FirstOrDefault(u => u.Username == username);

            if (user == null || user.Password != password || user.IsDeleted)
            {
                throw new InvalidOperationException("Invalid username or password.");
            }

            return user;
        }

        public User DeleteUser(string username, string password)
        {
            var user = GetUserByCredentials(username, password);

            this.unitOfWork
                .GetRepository<User>()
                .Delete(user);
            this.unitOfWork.SaveChanges();

            return user;
        }

        private bool IsUserExisting(string username)
        {
            return this.unitOfWork
                    .GetRepository<User>()
                    .All()
                    .Any(u => u.Username == username);
        }

        private bool IsEmailExisting(string email)
        {
            return this.unitOfWork
                    .GetRepository<User>()
                    .All()
                    .Any(u => u.Email == email);
        }
    }
}
