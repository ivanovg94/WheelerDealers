using Dealership.Data.CompositeModels;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Controllers;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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


        [TestMethod]
        public void ThrowArgumentException_WhenInvalidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.LoadCars(-1, -1, -1, -1));

        }

        [TestMethod]
        public void ReturnViewResult_WhenValidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                         gearTypeServiceMock.Object, modelServiceMock.Object,
                                        userServiceMock.Object, mgrMock.Object);
            var carSearchResult = new CarSearchResult() { FoundCars = new List<CarSummary>(), TotalCount = 6 };
            this.carServiceMock.Setup(x => x.GetCarSearchResult(1, 1, 1, 1)).Returns(carSearchResult);

            //Act && Assert
            var result = sut.LoadCars(1, 1, 1, 1);

            //Assert 
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType_WhenValidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                       gearTypeServiceMock.Object, modelServiceMock.Object,
                                      userServiceMock.Object, mgrMock.Object);
            var carSearchResult = new CarSearchResult() { FoundCars = new List<CarSummary>(), TotalCount = 6 };
            this.carServiceMock.Setup(x => x.GetCarSearchResult(1, 1, 1, 1)).Returns(carSearchResult);

            //Act && Assert
            var result = (PartialViewResult)sut.LoadCars(1, 1, 1, 1);

            Assert.IsInstanceOfType(result.Model, typeof(SearchResultViewModel));
        }

        [TestMethod]
        public void ReturnsValidModelWithValidData_WhenValidParametersArePassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                       gearTypeServiceMock.Object, modelServiceMock.Object,
                                      userServiceMock.Object, mgrMock.Object);

            var carSearchResult = new CarSearchResult()
            { FoundCars = new List<CarSummary>() { new CarSummary() }, TotalCount = 6 };
            this.carServiceMock.Setup(x => x.GetCarSearchResult(1, 1, 1, 1)).Returns(carSearchResult);

            //Act 
            var result = (PartialViewResult)sut.LoadCars(1, 1, 1, 1);

            var vm = (SearchResultViewModel)result.Model;
            var expected = 2;
            var actual = vm.NumberOfPages;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallCorrectServiceMethod_WhenValidDataIsPassed()
        {
            //Arrange
            var sut = new CarController(carServiceMock.Object, brandServiceMock.Object,
                                       gearTypeServiceMock.Object, modelServiceMock.Object,
                                      userServiceMock.Object, mgrMock.Object);

            var carSearchResult = new CarSearchResult()
            { FoundCars = new List<CarSummary>() { new CarSummary() }, TotalCount = 6 };
            this.carServiceMock.Setup(x => x.GetCarSearchResult(1, 1, 1, 1)).Returns(carSearchResult);

            var result = (PartialViewResult)sut.LoadCars(1, 1, 1, 1);

            carServiceMock.Verify(s => s.GetCarSearchResult(1, 1, 1, 1), Times.Once);
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
