using Dealership.Client.Commands.CRUD;
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
   public class ViewCardDetailsCommand_Should
    {
        [TestMethod]
        public void ThrowArgumentExcpetion_WhenEmptyCollectionIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var sut = new ViewCarDetailsCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[0];
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(args));
        }

        [TestMethod]
        public void ThrowFormatExcpetion_WhenNonIntegerIdIsPassed()
        {
            //Arrange
            var sessionMock = new Mock<IUserSession>();
            var serviceMock = new Mock<ICarService>();
            var sut = new ViewCarDetailsCommand(sessionMock.Object, serviceMock.Object);
            var args = new string[1] { "a"};
            //Act && Assert
            Assert.ThrowsException<FormatException>(() => sut.Execute(args));
        }

        //[TestMethod]
        //public void ThrowArgumentException_WhenNullDependenciesArePassed()
        //{
        //    //Arrange
            
        //    var sut = new ViewCarDetailsCommand(null, null);
        //    var args = new string[1] { "1" };
        //    //Act && Assert
       
        //    mock.Verify(h => h.AsyncHandle(It.Is<SomeResponse>(r => r != null)));
        //}


    }
}
