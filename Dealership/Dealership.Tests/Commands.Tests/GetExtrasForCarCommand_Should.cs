using Dealership.Client.Commands.CRUD.ExtrasCommands;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Tests.Commands.Tests
{
    [TestClass]
    public class GetExtrasForCarCommand_Should
    {
        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptyCollectionIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            sessionMock.Setup(s => s.CurrentUser.UserType).Returns(UserType.Admin);
            var serviceMock = new Mock<IExtraService>();
            var sut = new GetExtrasForCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[0];
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ThrowFormatExcpetion_WhenNonIntegerIdIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            sessionMock.Setup(s => s.CurrentUser.UserType).Returns(UserType.Admin);
            var serviceMock = new Mock<IExtraService>();
            var sut = new GetExtrasForCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[1] { "a" };
            //Act && Assert
            Assert.ThrowsException<FormatException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ReturnPropperMessage_WhenNoElementsAreFound()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            sessionMock.Setup(s => s.CurrentUser.UserType).Returns(UserType.Admin);
            var serviceMock = new Mock<IExtraService>();
            var emptyCollection = new List<Extra>();
            serviceMock.Setup(s => s.GetExtrasForCar(It.IsAny<int>())).Returns(emptyCollection);
            var sut = new GetExtrasForCarCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[1] { "1" };
            //Act 
            var actual = sut.Execute(args);
            var expected = "No extras.";
            //Assert
            Assert.IsTrue(actual == expected);
        }

    }
}
