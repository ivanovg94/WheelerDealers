using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Tests.CarService.Tests
{
    [TestClass]
    public class AddCar_Should
    {
        [TestMethod]
        public void ThrowServiceExcpetion_WhenNullArgumentIsPassed()
        {
            //arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            var sut = new Services.CarService(fakeUnitOfWork.Object);
            //act
            //assert
            Assert.ThrowsException<ServiceException>(() => sut.AddCar(null));
        }
        
        //[TestMethod]
        //public void AddCarToDatabase_WhenValidParametersArePassed()
        //{
        //    var contexOptions = new DbContextOptionsBuilder<DealershipContext>()
        //        .UseInMemoryDatabase(databaseName: "AddCarToDatabase_WhenValidParametersArePassed")
        //        .Options;

        //    var fakeUnitOfWork = new DealershipContext(contexOptions);
        //    var carService = new Services.CarService(fakeUnitOfWork.Object);

        //    var fakeCar = new Mock<Car>();

        //    carService.AddCar(fakeCar.Object);

            
        //}
    }
}
