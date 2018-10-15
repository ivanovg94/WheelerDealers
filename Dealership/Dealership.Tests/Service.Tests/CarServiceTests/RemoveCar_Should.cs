using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.CarServiceTests
{
    [TestClass]
    public class RemoveCar_Should
    {
        [TestMethod]
        public void RemoveCar_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            int carId = 1;
            var car = new Car() { Id = carId };
            var cars = new List<Car>() { car };

            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

            var sut = new CarService(unitOfWorkMock.Object);

            // Act
            sut.RemoveCar(carId);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Car>().Delete(It.IsAny<Car>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}