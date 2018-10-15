using Dealership.Client.Commands.CRUD;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Tests.Client.Tests.EditCommand.Tests
{
    [TestClass]
    public class Execute_Should
    {
        //  for later..
        //[TestMethod]////    Client.EditCommand
        //public void InvokeEditBrandInEditCarService_WhenValidParametersPassed()
        //{
        //    var validParameters = new string[3] { "Brand", "1", "test" };
        //    var editCarService = new Mock<IEditCarService>();
        //    editCarService.Setup(x => x.EditBrand(validParameters.Skip(1).ToArray())).Returns("brand edited");

        //    var userSessionMock = new Mock<IUserSession>();
        //    userSessionMock.SetupGet(x => x.CurrentUser.UserType).Returns(UserType.Admin);

        //    var editCommandMock = new Dealership.Client.Commands.CRUD
        //        .EditCommand(userSessionMock.Object, editCarService.Object);


        //    editCommandMock.Execute(validParameters);

        //    editCarService.Verify(x => x.EditBrand(validParameters), Times.Once());
        //}
    }
}
