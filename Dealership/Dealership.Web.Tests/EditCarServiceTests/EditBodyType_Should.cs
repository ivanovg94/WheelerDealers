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
    public class EditBodyType_Should
    {
        

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
                sut = new EditCarService(dealerShipContext, carServiceStub.Object);
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

                sut = new EditCarService(dealerShipContext, carServiceStub.Object);

                Assert.ThrowsException<ArgumentException>(() => sut.EditBodyType(validParameters));
            }
        }
    }
}
