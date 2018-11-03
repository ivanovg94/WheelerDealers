using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
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
            var model = new CarViewModel
            {
                Id = car.Id,
                ProductionDate = car.ProductionDate,
                Brand = car.Brand.Name,
                CarModel = car.Model,
                BodyType = car.BodyType.Name,
                Color = car.Color.Name,
                ColorType = car.Color.ColorType.Name,
                FuelType = car.FuelType.Name,
                GearBoxType = car.GearBox.GearType.Name,
                EngineCapacity = car.EngineCapacity,
                HorsePower = car.HorsePower,
                NumberOfGears = car.GearBox.NumberOfGears,
                Price = car.Price,
                CarsExtras = car.CarsExtras,
                BodyTypes = this.bodyTypeService.GetBodyTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                GearTypes = this.gearTypeService.GetGearTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                ColorTypes = this.colorTypeService.GetColorTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList(),
                FuelTypes = this.fuelTypeService.GetFuelTypes().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarViewModel car)
        {
            EditProductionDate(car);

            return RedirectToAction("Edit", "Car", new { car.Id });
        }

        public IActionResult EditBrand(int id, CarViewModel car)
        {
            EditProductionDate(car);

            return View();
        }
        public void EditProductionDate(CarViewModel car)
        {
            var newBody = bodyTypeService.GetBodyType(car.BodyType);
            var newBrand = brandService.GetBrand(car.Brand);
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


            var upCar = new Car()
            {
                Id = car.Id,
                BodyType = newBody,
                BodyTypeId = newBody.Id,
                Brand = newBrand,
                BrandId = newBrand.Id,
                CarsExtras = car.CarsExtras,
                Color = newColor,
                ColorId = newColor.Id,
                EngineCapacity = newEngineCapacity,
                FuelType = newFuelType,
                FuelTypeId = newFuelType.Id,
                GearBox = newGearbox,
                GearBoxId = newGearbox.Id,
                HorsePower = newHorsePower,
                Model = newModel,
                Price = newPrice,
                ProductionDate = newProductionDate,
                ModifiedOn = DateTime.Now,
                
            };
            
            carService.Update(upCar);
            
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

            var model = new BrowseViewModel()
            {
                Summaries = this.carService.GetCars(page * nPerPage, nPerPage)
                .Select(c => new CarSummaryViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand.Name,
                    CarModel = c.Model,
                    Capacity = c.EngineCapacity,
                    GearType = c.GearBox.GearType.Name,
                    Fuel = c.FuelType.Name,
                    Color = c.Color.Name
                }),
                Pages = pageCount,
                CurrentPage = page
            };

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
    }
}