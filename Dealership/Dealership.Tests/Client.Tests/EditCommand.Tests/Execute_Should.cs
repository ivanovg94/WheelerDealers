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
using System.Text;

namespace Dealership.Tests.Client.Tests.EditCommand.Tests
{
    [TestClass]
    public class Execute_Should
    {
        //[TestMethod]//     Client.EditCommand
        //public void InvokeEditBrandInEditCarService_WhenValidParametersPassed()
        //{
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    var contxt = new Mock<IDealershipContext>();
        //    var uow = new Mock<IUnitOfWork>();

        //    var CarRepositoryMock = new Mock<IRepository<ICar>>();
        //    var BrandRepositoryMock = new Mock<IRepository<IBrand>>();
        //    uow.Setup(x => x.GetRepository<ICar>()).Returns(CarRepositoryMock.Object);
        //    uow.Setup(x => x.GetRepository<IBrand>()).Returns(BrandRepositoryMock.Object);

        //    var carServiceMock = new Mock<ICarService>();

        //    var validParameters = new string[] { "Brand", "1", "BMW" };
        //    var EditCarServiceMock = new Mock<IEditCarService>();

        //    var uSessMock = new Mock<IUserSession>();
        //    uSessMock.SetupGet(x => x.CurrentUser.UserType).Returns(Data.Models.UserType.Admin);

        //    EditCarServiceMock.Setup(x => x.EditBrand(validParameters)).Returns("Brand edited");
        //    //EditCarServiceMock.SetupGet(x => x.CarService).Returns(carServiceMock.Object);
        //    //carServiceMock.Setup(x => x.GetCar(It.IsAny<int>())).Returns(new Mock<Data.Models.Car>().Object);

        //    var sut = new Mock<Dealership.Client.Commands.CRUD
        //        .EditCommand>(uSessMock.Object, EditCarServiceMock.Object);// (uSessMock.Object, EditCarServiceMock.Object);

        //   var result = sut.Object.Execute(validParameters);

        //    Assert.IsTrue(result.Contains("edited"));
        //}
    }
}
