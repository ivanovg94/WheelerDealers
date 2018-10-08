using Dealership.Client.Commands.Abstract;
using Dealership.Data.DataProcessor.Dto;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dealership.Client.Commands
{
    public class ImportCommand : PrimeCommand
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";
        private const string importDirRes = @"..\..\..\..\Dealership.Data\DataProcessor\ImportDatasets\";
        const string datasetDir = @"..\..\..\..\Dealership.Data\DataProcessor\ImportDatasets\";

        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            //var cars = ImportCars(File.ReadAllText(datasetDir + "cars1.json"));

            return $"Succesfully imported cars";
        }

        //public string ImportCars(string jsonString)
        //{
        //    var sb = new StringBuilder();

        //    var deserializedCars = JsonConvert.DeserializeObject<List<CarDto>>(jsonString, new JsonSerializerSettings
        //    {
        //        NullValueHandling = NullValueHandling.Ignore
        //    });

        //    var cars = new List<Car>();
        //    foreach (var carDto in deserializedCars)
        //    {
        //        Brand brand = carDto.BrandName;
        //        string model = carDto.Model;
        //        short horsePower = carDto.HorsePower;
        //        short engineCapacity = carDto.EngineCapacity;
        //        DateTime productionDate = carDto.ProductionDate;
        //        decimal price = carDto.Price;
        //        string chassis = carDto.Chassis;
        //        string color = carDto.Color;
        //        string fuelType = carDto.Fuel;
        //        string gearbox = carDto.GearBox;

        //        var car = CarService.CreateCar(brand, model, horsePower, engineCapacity, productionDate, price, chassis, color, fuelType, gearbox);

        //        //TODO: add logger
        //        //sb.AppendLine(string.Format(SuccessMessage, car.Brand + " " + car.Model));
        //    }

        //    //Context.Cars.AddRange(cars);
        //    Context.SaveChanges();

        //    var result = sb.ToString();
        //    return result;
        //}
    }
}
