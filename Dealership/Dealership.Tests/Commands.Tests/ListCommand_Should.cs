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

        //[TestMethod]
        //public void ReturnPropperData_WhenValidParametersAreParsed()
        //{
        //    //Arrange
        //    var sessionMock = new Mock<IUserSession>();
        //    var serviceMock = new Mock<ICarService>();
        //    var carOneMock = new Mock<Car>();
        //    var brandMock = new Mock<Brand>();
        //    brandMock.SetupGet(b => b.Name).Returns("brand");
        //    carOneMock.SetupGet(c => c.Id).Returns(1);
        //    carOneMock.SetupGet(c => c.Model).Returns("model");
        //    carOneMock.SetupGet(c => c.Brand).Returns(brandMock.Object);
        //    carOneMock.SetupGet(c => c.EngineCapacity).Returns(2000);
        //    carOneMock.SetupGet(c => c.IsSold).Returns(false);
        //    carOneMock.SetupGet(c => c.IsDeleted).Returns(false);
        //    carOneMock.SetupGet(c => c.Price).Returns(1000);
        //    carOneMock.SetupGet(c => c.ProductionDate).Returns(DateTime.Now);
        //    carOneMock.SetupGet(c => c.Id).Returns(1);

        //    var cars = new List<Car>() { carOneMock.Object,carTwoMock.Object};
        //    serviceMock.Setup(s => s.GetCars("asc")).Returns(cars);
        //    var sut = new ListCommand(sessionMock.Object, serviceMock.Object);
        //    var args = new string[2] { "all", "asc" };
        //    //Act 
        //    var expected = $"There are no cars to be listed! Create new or inport cars.";
        //    var actual = sut.Execute(args);
        //    // Assert
        //    Assert.AreEqual(expected, actual);
        //}

    }
}
