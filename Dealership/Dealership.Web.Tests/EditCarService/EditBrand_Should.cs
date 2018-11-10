using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditBrand_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyArrayIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            var invalidParameters = new string[0];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealerShipContext, carServiceStub.Object);
            }


            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                                     .UseInMemoryDatabase(databaseName:
                                     "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = null;

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealerShipContext, carServiceStub.Object);
            }

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBrand(invalidParameters));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenInvalidIdInParametersIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                                    .UseInMemoryDatabase(databaseName:
                                    "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = { "asd", "test" };

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealerShipContext, carServiceStub.Object);
            }
            //Act&assert
            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public async Task EditBrandCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string result;
            Car testCar;

            string[] validParameters = new string[2] { "1", "newBrand" };
            string expectedBrandName = validParameters[1];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var testBrand = new Brand() { Name = "testBrand" };
                var testNewBrand = new Brand() { Name = "newBrand" };
                var carModel = new CarModel() { Name = "test" };
                dealerShipContext.Brands.Add(testNewBrand).Context.SaveChanges();

                testCar = new Car() { Brand = testBrand, CarModel = carModel };
                dealerShipContext.Cars.Add(testCar).Context.SaveChanges();
                var carServiceStub = new Services.CarService(dealerShipContext);
                var editCarService = new Services.EditCarService(dealerShipContext, carServiceStub);
                

                result = await editCarService.EditBrand(validParameters);
            }

            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.Brand.Name == expectedBrandName);
        }


        [TestMethod]
        public void ThrowInvalidOperationException_WhenUnexistingBrandIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            Car testCar;

            string[] validParameters = new string[2] { "1", "unexistingBrand" };
            string expectedBrandName = validParameters[1];
            IEditCarService sut;
            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var testBrand = new Brand() { Name = "testBrand" };
                var testNewBrand = new Brand() { Name = "newBrand" };
                var carModel = new CarModel() { Name = "test" };
                dealerShipContext.Brands.Add(testNewBrand).Context.SaveChanges();


                testCar = new Car() { Brand = testBrand, CarModel = carModel };
                dealerShipContext.Cars.Add(testCar).Context.SaveChanges();
                
                
            }

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Services.CarService(dealerShipContext);
                sut = new Services.EditCarService(dealerShipContext, carServiceStub);
                Assert.ThrowsException<InvalidOperationException>(() => sut.EditBrand(validParameters));
            }
        }
        //[TestMethod]
        //public void CreateNewBrand_IfInputBrandNotExistsInDatabase()
        //{
        //    //arrange
        //    var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
        //        .UseInMemoryDatabase(databaseName:
        //        "EditModelCorrectly_WhenValidParametersArePassed").Options;

        //    string result;
        //    Car testCar;

        //    string[] validParameters = new string[2] { "1", "unexistingBrand" };
        //    string expectedBrandName = validParameters[1];

        //    //using (var dealerShipContext = new DealershipContext(contextOptions))
        //    //{
        //    //    var testBrand = new Brand() { Name = "testBrand" };

        //    //    var unitOfWork = new UnitOfWork(dealerShipContext);

        //    //    testCar = new Car() { Brand = testBrand };

        //    //    var carServiceStub = new Mock<ICarService>();
        //    //    carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

        //    //    var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

        //    //    result = editCarService.EditBrand(validParameters);
        //    //}

        //    using (var arrangeContext = new DealershipContext(contextOptions))
        //    {


        //        var unitOfWork = new UnitOfWork(arrangeContext);
        //        var testBrand = new Brand() { Name = "testBrand" };


        //        testCar = new Car() { Brand = testBrand, Model = "test" };
        //        arrangeContext.Cars.Add(testCar).Context.SaveChanges();

        //        var carServiceStub = new Mock<ICarService>();
        //        carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

        //        var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

        //        result = editCarService.EditBrand(validParameters);
        //    }


        //    Assert.IsTrue(testCar.Brand.Name == expectedBrandName);


        //}
    }
}

