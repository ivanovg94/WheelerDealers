using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.CarServiceTests
{
    [TestClass]
    public class RemoveCar_Should
    {
        //[TestMethod]
        //public void WithRepoRemoveCar_WhenPassedValidParameter()
        //{
        //    // Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();

        //    var car = new Car() { Id = 1 };
        //    var cars = new List<Car>();

        //    unitOfWorkMock.Setup(x => x.GetRepository<Car>().All()).Returns(cars.AsQueryable());

        //    var carsRepoMock = new Mock<IRepository<Car>>();
        //    //carsRepoMock.Setup(c => c.All()).Returns(cars.AsQueryable());


        //    //var carServiceMOck = new Mock<ICarService>();

        //    //carServiceMOck.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);

        //    //var carMock = new Mock<ICar>();
        //    //carMock.Setup(c => c.Id).Returns(0);

        //    //unitOfWorkMock.Setup(x => x.GetRepository<Car>()
        //    //.Delete(It.Is<Car>(c => c != null)))
        //    //.Callback(() => cars.RemoveAt(0));

        //    // Act
        //    var sut = new CarService(unitOfWorkMock.Object);
        //    sut.RemoveCar(car.Id);

        //    // Assert
        //    carsRepoMock.Verify(c => c.Delete(car), Times.Once);
        //    unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        //    //Assert.AreEqual(0, cars.Count);
        //}

        //[TestMethod]
        //public void RemoveCar_WhenPassedValidParameter()
        //{
        //    // Arrange
        //    var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
        //        .UseInMemoryDatabase(databaseName: "RemoveCar_WhenPassedValidParameter")
        //        .Options;

        //    var car = new Car()
        //    {
        //        Id = 1
        //    };

        //    // Act
        //    using (var actContext = new DealershipContext(contextOptions))
        //    {
        //        var unitofWork = new UnitOfWork();
        //        var carsRepo = new Repository<Car>(actContext);
        //        var sut = new CarService(unitofWork);
        //        sut.AddCar(car);

        //        sut.RemoveCar(1);
        //    }

        //    // Assert
        //    using (var assertContext = new DealershipContext(contextOptions))
        //    {
        //        Assert.IsTrue(assertContext.Cars.Count() == 0);
        //        //Assert.IsTrue(assertContext.Cars.Contains(post));
        //    }
        //}


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