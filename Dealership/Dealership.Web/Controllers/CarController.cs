﻿using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace Dealership.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IBrandService brandService;
        private readonly IBodyTypeService bodyTypeService;
        private readonly IColorTypeService colorTypeService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IGearTypeService gearTypeService;

        public CarController(ICarService carService, IBrandService brandService,
            IBodyTypeService bodyTypeService, IColorTypeService colorTypeService,
            IFuelTypeService fuelTypeService, IGearTypeService gearTypeService)
        {
            this.carService = carService;
            this.brandService = brandService;
            this.bodyTypeService = bodyTypeService;
            this.colorTypeService = colorTypeService;
            this.fuelTypeService = fuelTypeService;
            this.gearTypeService = gearTypeService;
        }



        public IActionResult Index()
        {
            return View();
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