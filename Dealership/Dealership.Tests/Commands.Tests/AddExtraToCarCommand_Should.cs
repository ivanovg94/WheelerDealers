using Dealership.Client.Commands.ExtrasCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Commands.Tests
{
    [TestClass]
    public class AddExtraToCarCommand_Should
    {
        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptyCollectionIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);
            var serviceMock = new Mock<IExtraService>();
            var sut = new AddExtraToCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[0];
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ThrowFormatExcpetion_WhenNonIntegerIdIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);
            var serviceMock = new Mock<IExtraService>();
            var sut = new AddExtraToCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[1] { "a" };
            //Act && Assert
            Assert.ThrowsException<FormatException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptySpaceIsPassedAsParameter()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var user = new User() { UserType = UserType.Admin };
            sessionMock.Setup(s => s.CurrentUser).Returns(user);
            var serviceMock = new Mock<IExtraService>();
            var sut = new AddExtraToCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[2] { "1", "" };
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }
    }
}
