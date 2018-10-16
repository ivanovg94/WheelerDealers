using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.UserCommands
{
    public class RemoveCarFromFavoritesCommand : Command
    {
        private readonly IUserService userService;

        public RemoveCarFromFavoritesCommand(IUserSession userSession, IUserService userService) : base(userSession)
        {
            this.userService = userService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("Invalid parameters!");
            }

            int carId = int.Parse(parameters[0]);

            var car = this.userService.RemoveCarFromFavorites(carId, this.UserSession.CurrentUser.Username);

            return $"Car with Id {car.Id} was removed successfully from favorites.";
        }
    }
}
