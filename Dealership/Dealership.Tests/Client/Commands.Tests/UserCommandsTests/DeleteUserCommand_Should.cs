using Dealership.Client.Commands.UserCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dealership.Tests.Commands.Tests.UserCommandsTests
{
    [TestClass]
    public class DeleteUserCommand_Should
    {
        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var currentUser = new User() { UserType = UserType.User };
            sessionMock.Setup(s => s.CurrentUser).Returns(currentUser);

            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            var user = new User() { Id = 1 };
            userServiceMock
                .Setup(c => c.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var sut = new DeleteUserCommand(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[2] { "1", "pass" };

            // Act
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "successfully deleted");
        }
    }
}
