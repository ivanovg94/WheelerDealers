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
        private readonly IModelService modelService;
        private readonly IColorService colorService;

        public CarController(ICarService carService, IEditCarService editCarService, IBrandService brandService,
            IBodyTypeService bodyTypeService, IColorTypeService colorTypeService,
            IFuelTypeService fuelTypeService, IGearTypeService gearTypeService,
            IModelService modelService, IColorService colorService)
        {
            this.carService = carService;
            this.editCarService = editCarService;
            this.brandService = brandService;
            this.bodyTypeService = bodyTypeService;
            this.colorTypeService = colorTypeService;
            this.fuelTypeService = fuelTypeService;
            this.gearTypeService = gearTypeService;
            this.modelService = modelService;
            this.colorService = colorService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            //var list = this.carService.GetCars("asc");

            //return View(list);
            return null;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = this.carService.GetCar(id);
            var carVm = new CarViewModel(car);
            var model = new EditCarViewModel
            {
                Brands = this.brandService.GetBrands()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                CarModels = this.modelService.GetAllModelsByBrandId(car.BrandId)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                NumberOfGears = this.gearTypeService.GetGearboxesDependingOnGearType(id)
               .Select(x => new SelectListItem { Value = x.NumberOfGears.ToString(), Text = x.NumberOfGears.ToString() }).ToList(),

                BodyTypes = this.bodyTypeService.GetBodyTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                GearTypes = this.gearTypeService.GetGearTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                ColorTypes = this.colorTypeService.GetColorTypes()
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                FuelTypes = this.fuelTypeService.GetFuelTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                Car = carVm
            };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCarViewModel model)
        {
            EditCar(model.Car);

            return RedirectToAction("Details", "Car", new { model.Car.Id });
        }

        //method
        public void EditCar(CarViewModel model)
        {
            var realCar = carService.GetCar(model.Id);

            var newBody = bodyTypeService.GetBodyType(model.BodyTypeId);
            var newBrand = brandService.GetBrand(model.BrandId);
            var newModel = brandService.GetModeldOfBrand(model.BrandId, model.CarModelId);

            var newColor = this.colorService.GetColor(model.Color, model.ColorTypeId);
            if (newColor == null)
            {
                newColor = this.colorService.AddColor(model.Color, model.ColorTypeId);
            }
            var newEngineCapacity = model.EngineCapacity;
            var newFuelType = fuelTypeService.GetFuelType(model.FuelTypeId);
            var newGearbox = this.gearTypeService.GetGearBox(model.GearBoxTypeId, model.NumberOfGears);
            var newHorsePower = model.HorsePower;
            var newPrice = model.Price;
            var newProductionDate = model.ProductionDate;
            //    var newImageName = model.ImageUrl;

            realCar.BodyType = newBody;
            realCar.BodyTypeId = newBody.Id;
            realCar.Brand = newBrand;
            realCar.BrandId = newBrand.Id;
            realCar.Color = newColor;
            realCar.ColorId = newColor.Id;
            realCar.EngineCapacity = newEngineCapacity;
            realCar.FuelType = newFuelType;
            realCar.FuelTypeId = newFuelType.Id;
            realCar.GearBox = newGearbox;
            realCar.GearBoxId = newGearbox.Id;
            realCar.HorsePower = newHorsePower;
            realCar.CarModelId = newModel.Id;
            realCar.CarModel = newModel;
            realCar.Price = newPrice;
            realCar.ProductionDate = newProductionDate;
            realCar.ModifiedOn = DateTime.Now;

            carService.Update(realCar);
        }

        [HttpGet]
        public IActionResult DeleteAction(bool confirm, int id)
        {
            if (confirm)
            {
                var removedCar = carService.RemoveCar(id);
            }
            return RedirectToAction("Browse", "Car");

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
                    Mileage = $"{c.Mileage} miles"
                }),
                Pages = pageCount,
                CurrentPage = page
            };
            model.Brands
                .AddRange(this.brandService.GetBrands()
                .Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList());


            //model.CarModels
            //   .AddRange(this.brandService.GetBrandModels(model.)
            //   .Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList());

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EditCarViewModel
            {
                Brands = this.brandService.GetBrands()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                CarModels = this.modelService.GetAllModelsByBrandId(this.brandService.GetBrands().FirstOrDefault().Id)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                NumberOfGears = this.gearTypeService.GetGearboxesDependingOnGearType(this.gearTypeService.GetGearTypes().FirstOrDefault().Id)
               .Select(x => new SelectListItem { Value = x.NumberOfGears.ToString(), Text = x.NumberOfGears.ToString() }).ToList(),

                BodyTypes = this.bodyTypeService.GetBodyTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                GearTypes = this.gearTypeService.GetGearTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                ColorTypes = this.colorTypeService.GetColorTypes()
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                FuelTypes = this.fuelTypeService.GetFuelTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                Car = new CarViewModel()
                {
                    StatusMessage = this.StatusMessage
                }
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditCarViewModel model)
        {
            var car = this.carService.CreateCar(
                model.Car.BrandId, model.Car.CarModelId, model.Car.Mileage, model.Car.HorsePower,
                model.Car.EngineCapacity, model.Car.ProductionDate, model.Car.Price,
                model.Car.BodyTypeId, model.Car.Color, model.Car.ColorTypeId, model.Car.FuelTypeId,
                model.Car.GearBoxTypeId, model.Car.NumberOfGears);

            this.carService.AddCar(car);

            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    if (!this.IsValidImage(image))
                    {
                        this.StatusMessage = "Error: Please provide a.jpg or .png file smaller than 5MB";
                        return this.RedirectToAction(nameof(Create));
                    }
                }

                this.carService.SaveImages(
                         this.GetUploadsRoot(),
                         model.Images.Select(i => i.FileName).ToList(),
                         model.Images.Select(i => i.OpenReadStream()).ToList(),
                         car.Id
                     );
            }

            this.StatusMessage = "Car registration is successful!";
            return RedirectToAction("Details", "Car", new { id = car.Id });
        }

        public JsonResult GetModelsByBrandId(int brandId)
        {
            var list = this.modelService.GetAllModelsByBrandId(brandId);
            return Json(list);
        }

        public JsonResult GetGearsDependingOnGearBoxType(int id)
        {
            var list = (this.gearTypeService.GetGearboxesDependingOnGearType(id));
            return Json(list);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var car = this.carService.GetCar(id);
            var model = new CarViewModel(car)
            {
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            this.carService.RemoveCar(id);

            return RedirectToAction(nameof(Browse));
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
            return image.Length <= 5242880;
        }
    }
}