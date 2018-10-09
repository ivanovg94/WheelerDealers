using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Services.Abstract;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Dealership.Client.Commands
{
    public class ExportCommand : PrimeCommand
    {
        //export sold asc
        //export available 
        const string exportDirRes = @"..\..\..\..\Dealership.Data\DataProcessor\ExportResults\";

        public ICarService CarService { get; set; }

        public ExportCommand()
        {
        }

        public override string Execute(string[] parameters)
        {
            var jsonOutput = Serializer(parameters);
            File.WriteAllText(exportDirRes + "cars.json", jsonOutput);

            return $"Successfully exported cars!";
        }

        public string Serializer(string[] parameters)
        {
            bool isSold = false;
            if (parameters.Length == 2)
            {
                isSold = true;
            }

            string order = parameters.Last();

            var cars = CarService.GetCars(isSold, order);

            var result = cars.Select(c => new CarVM
            {
                Id = c.Id,
                BrandName = c.Brand.Name,
                Model = c.Model,
                EngineCap = c.EngineCapacity,
                HorsePower = c.HorsePower,
                ProductionDate = c.ProductionDate,
                Price = c.Price,
                Chassis = c.Chasis.Name,
                NDoors = c.Chasis.NumberOfDoors,
                Color = c.Color.Name,
                ColorType = c.Color.ColorType.Name,
                Fuel = c.FuelType.Name,
                Gearbox = c.GearBox.GearType.Name,
                NumberOfGears = c.GearBox.NumberOfGears,
                Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
            }).ToList();

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;
        }
    }
}
