using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Controllers;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Dealership.Web.Tests.Controllers.UserControllerTests
{
    [TestClass]
    public class Favorites_Should
    {
        [TestMethod]
        public void ReturnsViewResult_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userManagerMock = SetupUserManagerMock();

            var controller = new UserController(userServiceMock.Object, userManagerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal()
                    }
                },
                TempData = new Mock<ITempDataDictionary>().Object
            }; ;

            var car = new Car() { Id = 1 };
            var cars = new List<Car>() { car };

            userServiceMock.Setup(uc => uc.GetFavorites(It.IsAny<User>())).Returns(cars);

            // Act
            var result = controller.Favorites();
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewModel_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = SetupController(userServiceMock);

            var car = new Car() { Id = 1 };
            var cars = new List<Car>() { car };

            userServiceMock.Setup(uc => uc.GetFavorites(It.IsAny<User>())).Returns(cars);

            // Act
            var result = controller.Favorites() as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<CarSummaryViewModel>));
        }

        [TestMethod]
        public void InvokeCorrectServiceMethod_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = SetupController(userServiceMock);

            var car = new Car() { Id = 1 };
            var cars = new List<Car>() { car };

            userServiceMock.Setup(uc => uc.GetFavorites(It.IsAny<User>())).Returns(cars);

            // Act
            var sut = controller.Favorites();

            // Assert
            userServiceMock.Verify(x => x.GetFavorites(It.IsAny<User>()), Times.Once);
        }

        private UserController SetupController(Mock<IUserService> userServiceMock)
        {
            var userManagerMock = SetupUserManagerMock();

            var controller = new UserController(userServiceMock.Object, userManagerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal()
                    }
                },
                TempData = new Mock<ITempDataDictionary>().Object
            }; ;
            
            return controller;
        }

        private Mock<UserManager<User>> SetupUserManagerMock()
        {
            return new Mock<UserManager<User>>(
                  new Mock<IUserStore<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<User>>().Object,
                  new IUserValidator<User>[0],
                  new IPasswordValidator<User>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<User>>>().Object);
        }
    }
}
