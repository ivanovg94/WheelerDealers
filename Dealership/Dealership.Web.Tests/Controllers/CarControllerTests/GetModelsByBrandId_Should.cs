using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Web.Tests.Controllers.CarControllerTests
{
    [TestClass]
    public class GetModelsByBrandId_Should
    {
        private Mock<ICarService> carServiceMock;
        private Mock<IBrandService> brandServiceMock;
        private Mock<IModelService> modelServiceMock;
        private Mock<IGearTypeService> gearTypeServiceMock;
        private Mock<IUserService> userServiceMock;
        private Mock<UserManager<User>> mgrMock;



        [TestMethod]
        public void ReturnJsonResult_WhenValidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);


            this.modelServiceMock.Setup(x => x.GetAllModelsByBrandId(1))
                .Returns(new List<CarModel>() { new CarModel() { Id = 1, Name = "model", BrandId = 1 } });

            //Act && Assert
            var result = sut.GetModelsByBrandId(It.IsAny<int>());

            //Assert 
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }


        [TestMethod]
        public void ReturnJson_WhenValidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);


            this.modelServiceMock.Setup(x => x.GetAllModelsByBrandId(1))
                .Returns(new List<CarModel>() { new CarModel() { Id = 1, Name = "model", BrandId = 1 } });

            //Act
            var actual = sut.GetModelsByBrandId(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(actual);
        }


        [TestMethod]
        public void CallCorrectServiceMethod_WhenValidDataIsPassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);

            this.modelServiceMock.Setup(x => x.GetAllModelsByBrandId(1))
                          .Returns(new List<CarModel>() { new CarModel() { Id = 1, Name = "model", BrandId = 1 } });

            //Act
            var actual = sut.GetModelsByBrandId(It.IsAny<int>());

            //Assert
            modelServiceMock.Verify(s => s.GetAllModelsByBrandId(It.IsAny<int>()), Times.Once);
        }

        [TestInitialize]
        public void Setup()
        {
            this.carServiceMock = new Mock<ICarService>();
            this.brandServiceMock = new Mock<IBrandService>();
            this.modelServiceMock = new Mock<IModelService>();
            this.gearTypeServiceMock = new Mock<IGearTypeService>();
            this.userServiceMock = new Mock<IUserService>();
            var storeMock = new Mock<IUserStore<User>>();
            this.mgrMock = new Mock<UserManager<User>>(storeMock.Object, null, null, null, null, null, null, null, null);
        }

    }
}
