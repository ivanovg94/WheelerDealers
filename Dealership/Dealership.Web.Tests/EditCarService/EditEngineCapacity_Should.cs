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
    public class EditEngineCapacity_Should
    {
        [TestMethod]
        public async Task ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Dealership.Services.EditCarService(dealershipContext, carServiceStub.Object);

            }
            var invalidParameters = new string[0];


            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = null;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public async Task ThowArgumentException_WhenInvalidIDIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                            .UseInMemoryDatabase(databaseName:
                            "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            string[] validParameters = { "invalidID", "test" };

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);

            }

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.EditEngineCapacity(validParameters));
        }
        [TestMethod]
        public async Task EditEngineCapacityValueCorrectly_WhenValidParametersArePassed()
        {
            var testCar = new Car()
            {
                Brand = new Brand() { Name = "test" },
                CarModel = new CarModel() { Name = "test" },
                EngineCapacity = 1000
            };

            var validParameters = new string[2] { "1", "4444" };
            var expectedValue = int.Parse(validParameters[1]);

            string result;

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carService = new Services.CarService(dealershipContext);

                dealershipContext.Cars.Add(testCar).Context.SaveChanges();

                sut = new Services.EditCarService(dealershipContext, carService);

                result = await sut.EditEngineCapacity(validParameters);
            }
            //assert    
            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.EngineCapacity == expectedValue);
        }
    }
}
