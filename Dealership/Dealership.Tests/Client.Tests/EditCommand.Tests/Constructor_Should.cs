using Dealership.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Dealership.Services.Abstract;

namespace Dealership.Tests.Client.Tests.EditCommand.Tests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange
            UserSession invalidUserSession = null;
            var editCarServiceMock = new Mock<IEditCarService>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dealership.Client.Commands.CRUD.EditCommand(invalidUserSession,editCarServiceMock.Object));
        }
        [TestMethod]
        public void NotThrowException_WhenValidUserSessionPassed()
        {
            //arrange
            var UserSessionMock = new Mock<UserSession>();
            var editCarServiceMock = new Mock<IEditCarService>();
            //act&assert
            new Dealership.Client.Commands.CRUD.EditCommand(UserSessionMock.Object, editCarServiceMock.Object);
        }
    }
}
