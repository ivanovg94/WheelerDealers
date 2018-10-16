using Dealership.Client.Commands.CRUD.FilterCarsCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Dealership.Tests.Commands.Tests.FilterCarsCommandsTests
{
    [TestClass]
    public class FilterByBrandCommand_Should
    {
        [TestMethod]
        public void ReturnMessageWithCars_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();
            var brandServiceMock = new Mock<IBrandService>();

            var sut = new FilterByBrandCommand(sessionMock.Object, brandServiceMock.Object, carServiceMock.Object);

            var brand = new Brand() { Name = "mercedes" };
            var bodyType = new BodyType() { Name = "bodyType" };
            var colorType = new ColorType() { Name = "colorType" };
            var color = new Color() { Name = "color", ColorType = colorType };
            var fuel = new FuelType() { Name = "fuel" };
            var gearType = new GearType() { Name = "gearType" };
            var gearbox = new Gearbox() { GearType = gearType };

            var car = new Car()
            {
                Brand = brand,
                BodyType = bodyType,
                Color = color,
                FuelType = fuel,
                GearBox = gearbox
            };
            var cars = new List<Car>() { car };

            brandServiceMock.Setup(b => b.GetBrand(It.IsAny<string>())).Returns(brand);
            carServiceMock.Setup(c => c.GetCars(It.IsAny<string>())).Returns(cars);

            // Act
            var parameters = new string[1] { "mercedes" };
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "mercedes");
        }

        [TestMethod]
        public void ReturnNoCarsMessage_WhenThereAreNoCarsWithThatBrand()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();
            var brandServiceMock = new Mock<IBrandService>();

            var sut = new FilterByBrandCommand(sessionMock.Object, brandServiceMock.Object, carServiceMock.Object);

            var brand = new Brand() { Name = "mercedes" };
            var bodyType = new BodyType() { Name = "bodyType" };
            var colorType = new ColorType() { Name = "colorType" };
            var color = new Color() { Name = "color", ColorType = colorType };
            var fuel = new FuelType() { Name = "fuel" };
            var gearType = new GearType() { Name = "gearType" };
            var gearbox = new Gearbox() { GearType = gearType };

            var car = new Car()
            {
                Brand = brand,
                BodyType = bodyType,
                Color = color,
                FuelType = fuel,
                GearBox = gearbox
            };
            var cars = new List<Car>() { car };

            brandServiceMock.Setup(b => b.GetBrand(It.IsAny<string>())).Returns(brand);
            carServiceMock.Setup(c => c.GetCars(It.IsAny<string>())).Returns(cars);

            // Act
            var parameters = new string[1] { "invalidBrand" };
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "no cars");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPassedInvalidParametersCount()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();
            var brandServiceMock = new Mock<IBrandService>();

            var sut = new FilterByBrandCommand(sessionMock.Object, brandServiceMock.Object, carServiceMock.Object);

            // Act && Assert
            var parameters = new string[0] { };
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}

