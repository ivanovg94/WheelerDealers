using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Web.Tests.Controllers.CarControllerTests
{
    [TestClass]
    public class LoadCars_Should
    {
        private Mock<ICarService> carServiceMock;
        private Mock<IBrandService> brandServiceMock;
        private Mock<IModelService> modelServiceMock;
        private Mock<IGearTypeService> gearTypeServiceMock;
        private Mock<IUserService> userServiceMock;
        private Mock<UserManager<User>> mgrMock;

        [TestInitialize]
        private void Setup()
        {
            this.carServiceMock = new Mock<ICarService>();
            this.brandServiceMock = new Mock<IBrandService>();
            this.modelServiceMock = new Mock<IModelService>();
            this.gearTypeServiceMock = new Mock<IGearTypeService>();
            this.userServiceMock = new Mock<IUserService>();
            var storeMock = new Mock<IUserStore<User>>();
            this.mgrMock = new Mock<UserManager<User>>(storeMock.Object, null, null, null, null, null, null, null, null);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.LoadCars(-1, 1, 1, 1));
            Assert.ThrowsException<ArgumentException>(() => sut.LoadCars(1, -1, 1, 1));
            Assert.ThrowsException<ArgumentException>(() => sut.LoadCars(1, 1, -1, 1));
            Assert.ThrowsException<ArgumentException>(() => sut.LoadCars(1, 1, 1, -1));
        }
    }
}
