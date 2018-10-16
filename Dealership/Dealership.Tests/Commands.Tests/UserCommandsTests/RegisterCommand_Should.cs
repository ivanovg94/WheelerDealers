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
    public class RegisterCommand_Should
    {
        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedValidParameters()
        {    
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var userServiceMock = new Mock<IUserService>();

            var user = new User() { Id = 1 };
            userServiceMock
                .Setup(c => c.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var sut = new RegisterCommand(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[4] { "username", "pass", "pass", "email" };

            // Act
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "registered succesfully");
        }

        [TestMethod]
        public void ThrowInvalidOperationException_WhenThereIsLoggedInUser()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var currentUser = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(currentUser);

            var userServiceMock = new Mock<IUserService>();
            
            var sut = new RegisterCommand(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[4] { "username", "pass", "pass", "email" };

            // Act && Assert
            Assert.ThrowsException<InvalidOperationException>(() => sut.Execute(parameters));
        }


        [TestMethod]
        public void ThrowArgumentException_WhenPassedInvalidParametersCount()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();           
            var userServiceMock = new Mock<IUserService>();

            var sut = new RegisterCommand(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[3] { "username", "pass", "pass" };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
