using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditBodyType_Should
    {
        [TestMethod]
        public async void EditBodyTypeCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string result;
            Car testCar;

            string[] validParameters = new string[2] { "1", "Sedan" };
            string expectedBodyTypeName = validParameters[1];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var testBody = new BodyType() { Name = "Sedan" };

                var carModel = new CarModel() { Name = "test" };
                
                testCar = new Car()
                {
                    Brand = new Brand() { Name = "test" },
                    CarModel = carModel,
                    BodyType = new BodyType() { Name = "Coupe" }
                };

                dealerShipContext.Cars.Add(testCar);
                dealerShipContext.BodyTypes.Add(testBody).Context.SaveChanges();

                var carServiceStub = new Services.CarService(dealerShipContext);
                
                var editCarService = new Services.EditCarService(dealerShipContext, carServiceStub);

                result = await editCarService.EditBodyType(validParameters);
            }

            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.BodyType.Name == expectedBodyTypeName);
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealerShipContext, carServiceStub.Object);
            }

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBodyType(null));
        }

        [TestMethod]
        public void ThowArgumentException_WhenInvalidIDIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
               .UseInMemoryDatabase(databaseName:
               "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {

                string[] validParameters = { "invalidID", "test" };
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealerShipContext, carServiceStub.Object);

                Assert.ThrowsException<ArgumentException>(() => sut.EditBodyType(validParameters));
            }
        }
    }
}
