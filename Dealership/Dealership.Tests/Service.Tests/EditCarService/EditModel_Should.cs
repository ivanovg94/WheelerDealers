using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class EditModel_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => editCarService.EditModel(null));
        }
        [TestMethod]
        public void ThrowArgumentNullException_WhenNoArgumentArePassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var emptyArray = new string[3];

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => editCarService.EditModel(emptyArray));
        }
        [Ignore]
        [TestMethod]
        public void EditModelCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var testCar = new Car()
            {
                Brand = new Brand() { Name = "test" },
                Model = "316i",

            };

            var validParameters = new string[2] { "1", "330xi" };

            using (var context = new DealershipContext(contextOptions))
            {

                //context.Cars.Add(testCar);
                var repo = new Repository<Car>(context);
                var unitOfWork = new UnitOfWork(context);
                //context.Cars.Add(testCar);
                //repo.Add(testCar);
                //unitOfWork.GetRepository<Car>().Add(testCar);
                var count = context.Cars.Count();
                var carService = new Services.CarService(unitOfWork);
                var sut = new Services.EditCarService(unitOfWork, carService);
                carService.AddCar(testCar);
                // dont find car in context.local 
                sut.EditModel(validParameters);

            }
            //assert
        }
    }
}
