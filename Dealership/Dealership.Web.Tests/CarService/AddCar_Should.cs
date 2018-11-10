using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Web.Tests.CarService
{
    [TestClass]
    public class AddCar_Should
    {
        [TestMethod]
        public void ThrowServiceExcpetion_WhenNullArgumentIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            ICarService sut;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                sut = new Services.CarService(dealershipContext);
            }
            //act
            //assert
            Assert.ThrowsException<ServiceException>(() => sut.AddCar(null));
        }

        [TestMethod]
        public void AddCarToDatabase_WhenValidParametersArePassed()
        {
            var contexOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName: "AddCarToDatabase_WhenValidParametersArePassed").Options;

            var testCar = new Mock<Car>();

            DealershipContext dealershipContext;

            using (dealershipContext = new DealershipContext(contexOptions))
            {
                var carService = new Services.CarService(dealershipContext);

                carService.AddCar(testCar.Object);
            }
            using (dealershipContext = new DealershipContext(contexOptions))
            {
                Assert.IsTrue(dealershipContext.Cars.Count() == 1);
            }
        }
    }
}
