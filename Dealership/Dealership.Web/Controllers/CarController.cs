using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dealership.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IEditCarService editCarService;
        private readonly IBrandService brandService;
        private readonly IBodyTypeService bodyTypeService;
        private readonly IColorTypeService colorTypeService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IGearTypeService gearTypeService;

        public CarController(ICarService carService, IEditCarService editCarService, IBrandService brandService,
            IBodyTypeService bodyTypeService, IColorTypeService colorTypeService,
            IFuelTypeService fuelTypeService, IGearTypeService gearTypeService)
        {
            this.carService = carService;
            this.editCarService = editCarService;
            this.brandService = brandService;
            this.bodyTypeService = bodyTypeService;
            this.colorTypeService = colorTypeService;
            this.fuelTypeService = fuelTypeService;
            this.gearTypeService = gearTypeService;
        }

        public IActionResult Index()
        {
            var list = this.carService.GetCars("asc");

            return View(list);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = this.carService.GetCar(id);
            var model = new CarViewModel(car);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarViewModel car)
        {
            EditProductionDate(car);

            return RedirectToAction("Details", "Car", new { car.Id });
        }

        public IActionResult EditBrand(int id, CarViewModel car)
        {
            EditProductionDate(car);

            return View();
        }
        public void EditProductionDate(CarViewModel car)
        {
            var realCar = carService.GetCar(car.Id);

            var newBody = bodyTypeService.GetBodyType(car.BodyType);
            Brand newBrand;
            try
            {
                newBrand = brandService.GetBrand(car.Brand);
            }
            catch
            {
                newBrand = new Brand() { Name = car.Brand };
            }

            var newColor = new Color() { Name = car.Color };
            newColor.ColorType = colorTypeService.GetColorTypes().FirstOrDefault(c => c.Name == car.ColorType);
            var newEngineCapacity = car.EngineCapacity;
            var newFuelType = fuelTypeService.GetFuelTypes().FirstOrDefault(ft => ft.Name == car.FuelType);
            var newGearbox = new Gearbox()
            {
                GearType = gearTypeService.GetGearTypes().FirstOrDefault(gt => gt.Name == car.GearBoxType)
                ,
                NumberOfGears = car.NumberOfGears
            };
            var newHorsePower = car.HorsePower;
            var newModel = car.CarModel;
            var newPrice = car.Price;
            var newProductionDate = car.ProductionDate;

            realCar.Model = newModel;
            realCar.BodyType = newBody;
            realCar.BodyTypeId = newBody.Id;
            realCar.Brand = newBrand;
            realCar.BrandId = newBrand.Id;
            realCar.CarsExtras = car.CarsExtras;
            realCar.Color = newColor;
            realCar.ColorId = newColor.Id;
            realCar.EngineCapacity = newEngineCapacity;
            realCar.FuelType = newFuelType;
            realCar.FuelTypeId = newFuelType.Id;
            realCar.GearBox = newGearbox;
            realCar.GearBoxId = newGearbox.Id;
            realCar.HorsePower = newHorsePower;
            realCar.Model = newModel;
            realCar.Price = newPrice;
            realCar.ProductionDate = newProductionDate;
            realCar.ModifiedOn = DateTime.Now;


            carService.Update(realCar);

        }

        [HttpGet]
        public IActionResult Browse(int page)
        {
            var nPerPage = 5;
            var pageCount = 0;
            var totalCount = this.carService.GetCarsCount();
            var reminder = totalCount % nPerPage;
            if (reminder != 0) { pageCount = (totalCount / nPerPage) + 1; }
            else { pageCount = totalCount / nPerPage; }

            //Expression<Func<Car, bool>> searchCriteria = (Car car) => true;
            //searchCriteria = x => x.Brand.Name.Contains("");

            var model = new BrowseViewModel()
            {
                Summaries = this.carService.GetCars(page * nPerPage, nPerPage)
                .Select(c => new CarSummaryViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand.Name,
                    CarModel = c.CarModel.Name,
                    Capacity = c.EngineCapacity,
                    GearType = c.GearBox.GearType.Name,
                    Fuel = c.FuelType.Name,
                    Color = c.Color.Name,
                    Price = $"{c.Price}$",
                    Mileage = $" XXXX miles"
                }),
                Pages = pageCount,
                CurrentPage = page
            };
            model.Brands
                .AddRange(this.bodyTypeService.GetBodyTypes()
                .Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList());
            //model.CarModels
            //   .AddRange(this.bodyTypeService.GetBodyTypes()
            //   .Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList());

            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new CarViewModel
            {
                BodyTypes = this.bodyTypeService.GetBodyTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                GearTypes = this.gearTypeService.GetGearTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                ColorTypes = this.colorTypeService.GetColorTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                FuelTypes = this.fuelTypeService.GetFuelTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList()
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var car = this.carService.CreateCar(model.Brand, model.CarModel, model.HorsePower, model.EngineCapacity, model.ProductionDate, model.Price, model.BodyType, model.Color, model.ColorType, model.FuelType, model.GearBoxType, model.NumberOfGears);


                this.carService.AddCar(car);
                AddImage(model.Image, car.Id);
                this.TempData["Success-Message"] = "You published a new post!";

                return RedirectToAction("Details", "Car", new { id = car.Id });
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var car = this.carService.GetCar(id);
            var model = new CarViewModel(car);

            return this.View(model);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            this.carService.RemoveCar(id);

            return RedirectToAction(nameof(Browse));
        }


        private void AddImage(IFormFile avatarImage, int carId)
        {
            if (avatarImage == null)
            {
                return; /*this.RedirectToAction(nameof(Index));*/

            }

            //if (!this.IsValidImage(avatarImage))
            //{
            //    this.StatusMessage = "Error: Please provide a .jpg or .png file smaller than 1MB";
            //    throw this.RedirectToAction(nameof(Index));
            //}

            this.carService.SaveAvatarImage(
                this.GetUploadsRoot(),
                avatarImage.FileName,
                avatarImage.OpenReadStream(),
                carId
            );
        }

        private string GetUploadsRoot()
        {
            var environment = this.HttpContext.RequestServices
                .GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;

            return Path.Combine(environment.WebRootPath, "images", "cars");
        }

        private bool IsValidImage(IFormFile image)
        {
            string type = image.ContentType;
            if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
            {
                return false;
            }

            return image.Length <= 1024 * 1024;
        }
    }
}