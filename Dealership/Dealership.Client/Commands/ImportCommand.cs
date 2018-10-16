using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dealership.Client.Commands
{
    public class ImportCommand : AdminCommand
    {
        const string datasetDir = @"..\..\..\..\Dealership.Data\DataProcessor\ImportDatasets\";
        private readonly ICarService carService;

        public ImportCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }

        public override string Execute(string[] parameters)
        {
            base.Execute(parameters);
            string fileName = parameters[0];
            var carsCount = ImportCars(File.ReadAllText(datasetDir + fileName + ".json"));

            return $"Successfully imported {carsCount} cars!";
        }

        public int ImportCars(string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedCars = JsonConvert.DeserializeObject<List<CarVM>>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var cars = new List<Car>();
            foreach (var carDto in deserializedCars)
            {
                string brand = carDto.BrandName;
                string model = carDto.Model;
                short horsePower = carDto.HorsePower;
                short engineCapacity = carDto.EngineCap;
                DateTime productionDate = carDto.ProductionDate;
                decimal price = carDto.Price;
                string bodyType = carDto.BodyType;
                string color = carDto.Color;
                string colorType = carDto.ColorType;
                string fuelType = carDto.Fuel;
                string gearbox = carDto.Gearbox;
                byte numOfGears = carDto.NumberOfGears;

                var car = carService.CreateCar(brand, model, horsePower, engineCapacity, productionDate, price, bodyType, color, colorType, fuelType, gearbox, numOfGears);

                cars.Add(car);
            }

            if (cars.Count == 0)
            {
                throw new InvalidOperationException("There is no cars to be imported.");
            }

            this.carService.AddCars(cars);

            return cars.Count;
        }
    }
}
