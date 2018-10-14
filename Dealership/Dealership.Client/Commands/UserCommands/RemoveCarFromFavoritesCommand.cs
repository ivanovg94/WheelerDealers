using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    class RemoveCarFromFavoritesCommand : PrimeCommand
    {
        public IUserService UserService { get; set; }

        public RemoveCarFromFavoritesCommand(IUserSession userSession) : base(userSession)
        {
        }

        public override string Execute(string[] parameters)
        {
            int carId = int.Parse(parameters[0]);

            var car = this.UserService.RemoveCarFromFavorites(carId, this.UserSession.CurrentUser.Username);

            return $"Car with Id {car.Id} was removed successfully from favorites.";
        }
    }
}
