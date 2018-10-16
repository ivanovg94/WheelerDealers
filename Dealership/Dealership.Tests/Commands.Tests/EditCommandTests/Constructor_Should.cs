using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Commands.Tests.EditCommandTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullUserSessionIsPassed()
        {
            //arrange
            UserSession invalidUserSession = null;
            var editCarServiceStub = new Mock<IEditCarService>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dealership.Client.Commands.CRUD.EditCommand(invalidUserSession,editCarServiceStub.Object));
        }
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullEditCarServiceIsPassed()
        {
            //arrange
            var validUserSessionStub = new Mock<IUserSession>();
            IEditCarService nullEditCarService = null;
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dealership.Client.Commands.CRUD.EditCommand(validUserSessionStub.Object, nullEditCarService));
        }
        [TestMethod]
        public void NotThrowException_WhenValidUserSessionPassed()
        {
            //arrange
            var UserSessionMock = new Mock<IUserSession>();
            var editCarServiceMock = new Mock<IEditCarService>();
            //act&assert
            new Dealership.Client.Commands.CRUD.EditCommand(UserSessionMock.Object, editCarServiceMock.Object);
        }
        [TestMethod]
        public void AssignUserSessionCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var UserSessionMock = new Mock<IUserSession>();
            var editCarServiceMock = new Mock<IEditCarService>();
            //act
            var sut = new Dealership.Client.Commands.CRUD.EditCommand(UserSessionMock.Object, editCarServiceMock.Object);

            Assert.IsInstanceOfType(sut.UserSession,typeof(IUserSession));
        }

        //TODO: COMPILATION ERROR
        //[TestMethod]
        //public void AssignEditCarServiceCorrectly_WhenValidParametersArePassed()
        //{
        //    //arrange
        //    var UserSessionMock = new Mock<IUserSession>();
        //    var editCarServiceMock = new Mock<IEditCarService>();
        //    //act
        //    var sut = new Dealership.Client.Commands.CRUD.EditCommand(UserSessionMock.Object, editCarServiceMock.Object);

        //    Assert.IsInstanceOfType(sut.EditCarService, typeof(IEditCarService));
        //}
    }
}
