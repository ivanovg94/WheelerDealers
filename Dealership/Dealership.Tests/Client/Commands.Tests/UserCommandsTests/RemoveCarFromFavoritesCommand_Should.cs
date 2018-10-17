using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Dealership.Client.Commands.UserCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dealership.Tests.Commands.Tests.UserCommandsTests
{
    [TestClass]
    public class RemoveCarFromFavoritesCommand_Should
    {
        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedValidParameters()
        {
            //  Arrange
            var sessionMock = new Mock<IUserSession>();
            var currentUser = new User() { UserType = UserType.User };
            sessionMock.Setup(s => s.CurrentUser).Returns(currentUser);

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            
            var car = new Car() { Id = 1 };
            userServiceMock
                .Setup(c => c.RemoveCarFromFavorites(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(car);

            var sut = new RemoveCarFromFavoritesCommand(sessionMock.Object, userServiceMock.Object);
            var parameters = new string[1] { "1" };

            // Act
            var result = sut.Execute(parameters);

            // Assert
            StringAssert.Contains(result, "removed successfully");
        }
    }
}
