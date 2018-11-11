using Dealership.Data.Context;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Web.Tests.EditCarServiceTests
{
    [TestClass]
    public class EditBrand_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyArrayIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            var invalidParameters = new string[0];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new EditCarService(dealerShipContext, carServiceStub.Object);
            }


            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                                     .UseInMemoryDatabase(databaseName:
                                     "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = null;

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new EditCarService(dealerShipContext, carServiceStub.Object);
            }

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBrand(invalidParameters));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenInvalidIdInParametersIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                                    .UseInMemoryDatabase(databaseName:
                                    "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = { "asd", "test" };

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new EditCarService(dealerShipContext, carServiceStub.Object);
            }
            //Act&assert
            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        
    }
}

