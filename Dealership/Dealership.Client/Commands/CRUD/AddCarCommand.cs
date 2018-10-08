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

            short horesePower;
            if (!short.TryParse(parameters[2], out horesePower))
            {
                throw new ArgumentException("Invalid horse power value!");
            }

            short engineCapacity;
            if (!short.TryParse(parameters[3], out engineCapacity))
            {
                throw new ArgumentException("Invalid engine capacity value!");
            }

            DateTime prodDate;
            if (!DateTime.TryParse(parameters[4], out prodDate))
            {
                throw new ArgumentException("Invalid production date passed!");
            }

            decimal price;
            if (!decimal.TryParse(parameters[5], out price))
            {
                throw new ArgumentException("Invalid price value!");
            }
            var chassisName = parameters[6];
            var colorName = parameters[7];
            var colorType = parameters[8];
            var fuelType = parameters[9];
            var gearboxType = parameters[10];

            byte numberOfGears; 
            if (!byte.TryParse(parameters[11], out numberOfGears))
            {
                throw new ArgumentException("Invalid number of gears passed!");
            }

            var car = CarService.CreateCar(brandName, model, horesePower, engineCapacity, prodDate, price, chassisName, colorName, colorType, fuelType, gearboxType, numberOfGears);

            this.CarService.AddCar(car);

            return $"{car.Brand.Name} {car.Model} with Id:{car.Id} was added successfully";
        }
    }
}
