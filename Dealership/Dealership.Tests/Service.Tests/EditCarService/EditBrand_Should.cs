using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class EditBrand_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var invalidParameters = new string[0];

            var sut = new Dealership.Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = null;

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidIdInParametersIsPassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = new string[2] { "invalid", "test" };

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            //Act&assert
            Assert.ThrowsException<ArgumentException>(() => editCarService.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void EditBrandCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            var dealerShipContext = new DealershipContext(contextOptions);

            var testBrand = new Mock<Brand>();
            testBrand.Setup(tb => tb.Name).Returns("brand");

            var unitOfWork = new UnitOfWork(dealerShipContext);

            var testCar = new Car() { Brand = testBrand.Object };

            var carServiceStub = new Mock<ICarService>();
            carServiceStub.Setup(cs => cs.CreateCar(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<short>(), It.IsAny<short>(), It.IsAny<DateTime>()
                , It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<int>())).Returns(testCar);

            carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

            string[] validParameters = new string[2] { "1", "test" };
            string expectedBrandName = validParameters[1];

            var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

            var result = editCarService.EditBrand(validParameters);

            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.Brand.Name == expectedBrandName);
        }
    }
}
