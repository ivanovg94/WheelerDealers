using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
    public class UserService : IUserService
    {
        private readonly ICarService carService;
        private readonly DealershipContext dealershipContext;

        public UserService(ICarService carService,DealershipContext dealershipContext)
        {
            this.carService = carService;
            this.dealershipContext = dealershipContext;
        }

        public ICollection<User> GetUsers()
        {
            var users = this.dealershipContext.Users.Include(u=>u.UsersCars)
                                                    .ToList();
            return users;
        }
    }
}
