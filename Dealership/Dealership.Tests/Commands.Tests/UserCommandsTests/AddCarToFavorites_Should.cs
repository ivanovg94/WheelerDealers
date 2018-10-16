using Dealership.Client.Commands.UserCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Commands.Tests.UserCommandsTests
{
    [TestClass]
    public class AddCarToFavorites_Should
    {
        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var currentUser = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(currentUser);

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            
            var car = new Car() { Id = 1 };
            userServiceMock
                .Setup(c => c.AddCarToFavorites(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(car);

            var sut = new AddCarToFavorites(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[1] { "1" };

            // Act
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "added successfully");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPassedInvalidParametersCount()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var userServiceMock = new Mock<IUserService>();

            var sut = new AddCarToFavorites(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[2] { "1", "2" };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
