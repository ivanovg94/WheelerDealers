using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;

namespace Dealership.Client.Commands.CRUD
{
    public class RemoveCarCommand : AdminCommand
    {
        private readonly ICarService carService;

        public RemoveCarCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }

        public override string Execute(string[] parameters)
        {
            base.Execute(parameters);
            int carId = int.Parse(parameters[0]);
            this.carService.RemoveCar(carId);

            return $"Car with ID {carId} was removed!";
        }
    }
}
