using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.CRUD
{
    public class AddCarCommand : PrimeCommand
    {
        public ICarService CarService { get; set; }
        //add brand, model, hp, engCap, prod.date, price, chasis, nDoors, colorName,ColorType, fuelType, gearbox, nGears
        public override string Execute(string[] parameters)
        {
            //validation TODO
            var brandName = parameters[0];
            var model = parameters[1];

            if (!short.TryParse(parameters[2], out short horesePower)) { throw new ArgumentException("Invalid horse power value!"); }
            if (!short.TryParse(parameters[3], out short engineCapacity)) { throw new ArgumentException("Invalid engine capacity value!"); }
            if (!DateTime.TryParse(parameters[4], out DateTime prodDate)) { throw new ArgumentException("Invalid production date passed!"); }
            if (!decimal.TryParse(parameters[5], out decimal price)) { throw new ArgumentException("Invalid price value!"); }
            var chassisName = parameters[6];
            var colorName = parameters[7];
            var colorType = parameters[8];
            var fuelType = parameters[9];
            var gearboxType = parameters[10];

            if (!byte.TryParse(parameters[11], out byte numberOfGears)) { throw new ArgumentException("Invalid number of gears passed!"); }

            var car = CarService.CreateCar(brandName, model, horesePower, engineCapacity, prodDate, price, chassisName, colorName, colorType, fuelType, gearboxType, numberOfGears);

            CarService.AddCar(car);

            return $"{car.Brand.Name} {car.Model} with Id:{car.Id} was added successfully";
        }
    }
}
