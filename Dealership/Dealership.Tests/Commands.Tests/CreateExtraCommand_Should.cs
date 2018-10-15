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
   public class CreateExtraCommand_Should
    {
        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptyCollectionIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            sessionMock.Setup(s => s.CurrentUser.UserType).Returns(UserType.Admin);
            var serviceMock = new Mock<IExtraService>();
            var sut = new CreateExtraCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[0];
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }
    }
}
