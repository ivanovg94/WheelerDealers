using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditModel_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            //act&assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditModel(null));
        }
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenNoArgumentArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            var emptyArray = new string[3];
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            //act&assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.EditModel(emptyArray));
        }

        [TestMethod]
        public async void EditModelCorrectly_WhenValidParametersArePassed()
        {
            var testCar = new Car()
            {
                Brand = new Brand() { Name = "test" },
                CarModel = new CarModel() { Name = "test" }
            };

            var validParameters = new string[2] { "1", "330xi" };
            string result;

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
               .UseInMemoryDatabase(databaseName:
               "EditModelCorrectly_WhenValidParametersArePassed").Options;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                dealershipContext.Cars.Add(testCar).Context.SaveChanges();

                var carService = new Services.CarService(dealershipContext);

                var sut = new Services.EditCarService(dealershipContext, carService);

                result = await sut.EditModel(validParameters);
            }
            //assert    
            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.CarModel.Name == validParameters[1]);
        }
    }
}
