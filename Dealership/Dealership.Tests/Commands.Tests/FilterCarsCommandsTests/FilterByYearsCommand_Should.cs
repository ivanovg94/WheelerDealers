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
    public class FilterByYearsCommand_Should
    {
        [TestMethod]
        public void ReturnMessageWithCars_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);

            var brand = new Brand() { Name = "brand" };
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
                GearBox = gearbox,
                ProductionDate = new DateTime(2005, 10, 17)
            };

            var cars = new List<Car>() { car };

            carServiceMock.Setup(c => c.GetCars(It.IsAny<string>())).Returns(cars);

            // Act
            var parameters = new string[2] { "2004", "2006" };
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "Year: 2005");
        }

        [TestMethod]
        public void ReturnNoCarsMessage_WhenThereAreNoCarsInThatRange()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);

            var brand = new Brand() { Name = "brand" };
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
                GearBox = gearbox,
                ProductionDate = new DateTime(2005, 10, 17)
            };

            var cars = new List<Car>() { car };

            carServiceMock.Setup(c => c.GetCars(It.IsAny<string>())).Returns(cars);

            // Act
            var parameters = new string[2] { "2010", "2018" };
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "no cars");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMinYearIsBiggerThanMaxYear()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);

            // Act && Assert
            var parameters = new string[2] { "2018", "2000" };
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPassedInvalidParametersCount()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);

            // Act && Assert
            var parameters = new string[1] { "2000" };
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }

        [TestMethod]
        public void ThrowFormatExcpetion_WhenPassedNonDatetimeFirstYear()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);
            var parameters = new string[2] { "string", "2010" };

            // Act && Assert
            Assert.ThrowsException<FormatException>(() => sut.Execute(parameters));
        }

        [TestMethod]
        public void ThrowFormatExcpetion_WhenPassedNonDatetimeSecondYear()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);

            var carServiceMock = new Mock<ICarService>();

            var sut = new FilterByYearsCommand(sessionMock.Object, carServiceMock.Object);
            var parameters = new string[2] { "2010", "string" };

            // Act && Assert
            Assert.ThrowsException<FormatException>(() => sut.Execute(parameters));
        }
    }
}
