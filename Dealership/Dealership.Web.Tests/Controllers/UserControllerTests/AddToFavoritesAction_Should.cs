using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;

namespace Dealership.Web.Tests.Controllers.UserControllerTests
{
    [TestClass]
    public class AddToFavoritesAction_Should
    {
        [TestMethod]
        public void ReturnRedirectResult_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = SetupController(userServiceMock);
            int id = 1;
            var result = controller.AddToFavorites(id);

            // Act && Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Details", redirectResult.ActionName);
            Assert.AreEqual("Car", redirectResult.ControllerName);
            Assert.AreEqual(1, redirectResult.RouteValues.Values.Count);
        }

        [TestMethod]
        public void InvokeCorrectServiceMethod_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = SetupController(userServiceMock);

            int id = 1;
            // Act
            var sut = controller.AddToFavorites(id);

            // Assert
            userServiceMock.Verify(x => x.AddCarToFavorites(It.IsAny<int>(), It.IsAny<User>()), Times.Once);
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
