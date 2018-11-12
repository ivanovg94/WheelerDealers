using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Controllers;
using Dealership.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;

namespace Dealership.Web.Tests.Controllers.AdminControllerTests
{
    [TestClass]
    public class Delete_Should
    {
        private Mock<IFuelTypeService> fuelTypeServiceMock;
        private Mock<IColorTypeService> colorTypeServiceMock;
        private Mock<IBodyTypeService> bodyTypeServiceMock;
        private Mock<IGearTypeService> gearTypeServiceMock;
        private Mock<IModelService> modelServiceMock;
        private Mock<IUserService> userServiceMock;
        private Mock<ICarService> carServiceMock;
        private Mock<IBrandService> brandServiceMock;
        private Mock<IExtraService> extraServiceMock;
        private Mock<IColorService> colorServiceMock;

        [TestInitialize]
        public void Setup()
        {
            this.fuelTypeServiceMock = new Mock<IFuelTypeService>();
            this.colorTypeServiceMock = new Mock<IColorTypeService>();
            this.bodyTypeServiceMock = new Mock<IBodyTypeService>();
            this.gearTypeServiceMock = new Mock<IGearTypeService>();
            this.modelServiceMock = new Mock<IModelService>();
            this.userServiceMock = new Mock<IUserService>();
            this.carServiceMock = new Mock<ICarService>();
            this.brandServiceMock = new Mock<IBrandService>();
            this.extraServiceMock = new Mock<IExtraService>();
            this.colorServiceMock = new Mock<IColorService>();
        }

        [TestMethod]
        public void ReturnRedirectResult_WhenCalled()
        {
            // Arrange
            var controller = SetupController();

            // Act
            var result = controller.Delete(true, 1);

            //  Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Search", redirectResult.ActionName);
            Assert.AreEqual("Car", redirectResult.ControllerName);
        }

        [TestMethod]
        public void InvokeCorrectServiceMethod_WhenCalled()
        {
            // Arrange
            var controller = SetupController();

            // Act
            var result = controller.Delete(true, 1);

            // Assert
            carServiceMock.Verify(s => s.RemoveCar(It.IsAny<int>()), Times.Once);
        }

        private AdminController SetupController()
        {
            var controller = new AdminController(fuelTypeServiceMock.Object,
                colorTypeServiceMock.Object,
                bodyTypeServiceMock.Object,
                gearTypeServiceMock.Object,
                modelServiceMock.Object,
                userServiceMock.Object,
                carServiceMock.Object,
                brandServiceMock.Object,
                extraServiceMock.Object,
                colorServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal()
                    }
                },
                TempData = new Mock<ITempDataDictionary>().Object
            }; ;

            return controller;
        }
    }
}