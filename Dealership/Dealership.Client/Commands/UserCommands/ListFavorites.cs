using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;

namespace Dealership.Client.Commands.UserCommands
{
    public class ListFavorites : Command
    {
        private readonly IUserService userService;

        public ListFavorites(IUserSession userSession, IUserService userService) : base(userSession)
        {
            this.userService = userService;
        }

        public override string Execute(string[] parameters)
        {
            var cars = this.userService.ListFavorites(this.UserSession.CurrentUser.Username);

            if (cars.Count == 0)
            {
                throw new InvalidOperationException("There is no cars in favorites.");
            }

            var result = cars.Select(c => new CarVM
            {
                Id = c.Id,
                BrandName = c.Brand.Name,
                Model = c.Model,
                EngineCap = c.EngineCapacity,
                HorsePower = c.HorsePower,
                ProductionDate = c.ProductionDate,
                Price = c.Price,
                BodyType = c.BodyType.Name,
                NDoors = c.BodyType.NumberOfDoors,
                Color = c.Color.Name,
                ColorType = c.Color.ColorType.Name,
                Fuel = c.FuelType.Name,
                Gearbox = c.GearBox.GearType.Name,
                NumberOfGears = c.GearBox.NumberOfGears,
                Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
            });

            return string.Join($"\r\n", result);
        }
    }
}
