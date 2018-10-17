using Dealership.Client.Commands.UserCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Commands.Tests.UserCommandsTests
{
    [TestClass]
    public class LogoutCommand_Should
    {
        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var currentUser = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(currentUser);

            var sut = new LogoutCommand(sessionMock.Object);
            var parameters = new string[0];

            // Act
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "successfully logged out");
        }

        [TestMethod]
        public void ThrowInvalidOperationException_WhenThereIsNoLoggedInUser()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();

            var sut = new LogoutCommand(sessionMock.Object);
            var parameters = new string[0];

            // Act && Assert
            Assert.ThrowsException<InvalidOperationException>(() => sut.Execute(parameters));
        }
    }
}
