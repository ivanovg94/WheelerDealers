using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;

namespace Dealership.Client.Commands.CRUD
{
    public class RemoveCarCommand : AdminCommand
    {
        public RemoveCarCommand(IUserSession userSession) : base(userSession)
        {
        }

        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            base.Execute(parameters);
            int carId = int.Parse(parameters[0]);
            this.CarService.RemoveCar(carId);

            return $"Car with ID {carId} was removed!";
        }
    }
}
