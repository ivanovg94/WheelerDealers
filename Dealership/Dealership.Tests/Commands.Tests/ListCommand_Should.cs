using Dealership.Client.Commands.CRUD;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Dealership.Tests.Commands.Tests
{
    [TestClass]
    public class ListCommand_Should
    {
        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptyCollectionIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var sut = new ListCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[0];
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ThrowArgumentExcpetion_WhenInvalidParametersArePassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var sut = new ListCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[1] { "invalid" };
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ReturnPropperMessage_WhenNoElementsAreFound()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var emptyCollection = new List<Car>();
            serviceMock.Setup(s => s.GetCars("asc")).Returns(emptyCollection);
            var sut = new ListCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[2] { "all", "asc" };
            //Act 
            var expected = $"There are no cars to be listed! Create new or inport cars.";
            var actual = sut.Execute(args);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnPropperData_WhenValidParametersAreParsed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var brand = new Brand() { Name = "brand" };
            var body = new BodyType() { Name = "body",NumberOfDoors=1 };
            var colorType = new ColorType() { Name = "type" };
            var color = new Color() { Name = "color", ColorType = colorType, ColorTypeId = 1 };
            var fuel = new FuelType() { Name = "fuel" };
            var gearType = new GearType() { Name = "auto" };
            var gear = new Gearbox() { GearType = gearType, NumberOfGears = 1 };

            var car = new Car()
            {
                Model = "model",
                HorsePower = 100,
                EngineCapacity = 1000,
                IsSold = false,
                Price = 1000,
                ProductionDate = DateTime.Parse("12.12.2012"),
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = body,
                Color = color,
                ColorId = 1,
                FuelType = fuel,
                FuelTypeId = 1,
                GearBox = gear,
                GearBoxId = 1
            };
            var carId = 0;
            var cars = new List<Car>() { car };
            serviceMock.Setup(s => s.GetCars("asc")).Returns(cars);
            var sut = new ListCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[2] { "all", "asc" };
            //Act 
            var expected =   
               $"Id:{carId} {brand.Name} {car.Model}, Engine: {car.EngineCapacity}cc {car.FuelType.Name} {car.HorsePower}hp, Body type {body.NumberOfDoors} door {body.Name}, Prod.: {car.ProductionDate.ToShortDateString()}, Price: {car.Price}, Color: {color.Name} {colorType.Name} Transmission: {gear.NumberOfGears} step {gear.GearType.Name} \r\nExtras: {string.Join(", ", car.CarsExtras)}\r\n";
            var actual = sut.Execute(args);
            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
