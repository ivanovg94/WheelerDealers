using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class AddCarToFavorites : PrimeCommand
    {
        public IUserService UserService { get; set; }

        public AddCarToFavorites(IUserSession userSession) : base(userSession)
        {
        }

        public override string Execute(string[] parameters)
        {
            int carId = int.Parse(parameters[0]);

            var car = this.UserService.AddCarToFavorites(carId, this.UserSession.CurrentUser.Username);

            return $"Car with Id {car.Id} was added successfully to favorites.";
        }
    }
}
