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
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullContextIsPassed()
        {
            //arrange
            var carServiceStub = new Mock<ICarService>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new EditCarService(null, carServiceStub.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullCarServiceIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
               .UseInMemoryDatabase(databaseName:
               "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var context = new DealershipContext(contextOptions);
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new EditCarService(context, null));
        }

        [TestMethod]
        public void InitializeEditCarsServiceCorrectly_WhenValidArgumentsArePassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var context = new DealershipContext(contextOptions);
            var carServiceStub = new Mock<ICarService>();

            var sut = new EditCarService(context, carServiceStub.Object);

            Assert.IsInstanceOfType(sut, typeof(IEditCarService));
        }

    }
}
