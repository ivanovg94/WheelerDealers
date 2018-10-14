using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //        .UseInMemoryDatabase(databaseName: "AddCarToDatabase_WhenValidParametersArePassed");

        //    var dealershipContext = new DealershipContext(contexOptions);
        //    var unitofWork = new UnitOfWork(dealershipContext);
        //    var carService = new Services.CarService(unitofWork);

        //    var testCar = new Mock<ICar>();

        //    carService.AddCar(testCar.Object);

        //    Assert.IsTrue(dealershipContext.Cars.Count() == 1);
        //}
    }
}
