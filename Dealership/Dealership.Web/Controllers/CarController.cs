using Dealership.Data.CompositeModels;
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

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


        public IActionResult LoadCars(int brandId, int modelId, int sort, int page)
        {
            var cars = this.carService
               .GetCarSearchResult(brandId, modelId, sort, page);

            var nPerPage = 5;
            var totalCount = cars.TotalCount;
            var reminder = totalCount % nPerPage;
            var pageCount = reminder != 0 ? (totalCount / nPerPage) + 1 : totalCount / nPerPage;

            var summaries = this.PopulateSummaries(cars.FoundCars);

            var searchResultVm = new SearchResultViewModel()
            {
                Summaries = summaries,
                NumberOfPages = pageCount,
                CurrentPage = page,
                SelectedBrandId = brandId,
                SelectedModelId = modelId,
                Sort = sort
            };

            // var summaries = this.PopulateSummaries(cars.FoundCars);
            return this.PartialView("_SearchResultPartial", searchResultVm);
        }

        public IActionResult Search(int brandId, int modelId, int sort, int page = 0)
        {
            var cars = this.carService.GetCarSearchResult(brandId, modelId, sort, page);

            var nPerPage = 5;
            var reminder = cars.TotalCount % nPerPage;
            var pageCount = reminder != 0 ? (cars.TotalCount / nPerPage) + 1 : cars.TotalCount / nPerPage;

            var searchVm = new SearchViewModel
            {
                SearchResult = new SearchResultViewModel()
                {
                    Summaries = this.PopulateSummaries(cars.FoundCars),
                    NumberOfPages = pageCount,
                    CurrentPage = 0,
                    SelectedBrandId = brandId,
                    SelectedModelId = modelId,
                    Sort = sort
                },

                Brands = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },
                CarModels = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },
                SortCriterias = new List<SelectListItem>() {
                     new SelectListItem { Value = "0", Text = "Published" },
                     new SelectListItem { Value = "1", Text = "Price Ascending" },
                     new SelectListItem { Value = "2", Text = "Price Descending" },
                     },
            };

            searchVm.Brands.AddRange(this.brandService.GetBrands()
                           .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList());

            return this.View(searchVm);
        }

        private IEnumerable<CarSummaryViewModel> PopulateSummaries(IEnumerable<CarSummary> cars)
        {
            return cars.Select(c => new CarSummaryViewModel()
            {
                Id = c.Id,
                Brand = c.Brand,
                CarModel = c.CarModel,
                Capacity = c.Capacity,
                GearType = c.GearType,
                Fuel = c.Fuel,
                Color = c.Color,
                Price = c.Price,
                Mileage = c.Mileage,
                ImageUrl = c.ImageUrl
            });
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
        public IActionResult Browse(int brandId, int modelId, int sort, int page)
        {
            var viewModel = PopulateBrowseViewModel(brandId, modelId, sort, page);

            return this.View(viewModel);
        }


        [HttpPost]
        public IActionResult Browse(BrowseViewModel model)
        {
            return RedirectToAction("Browse", "Car",
                new
                {
                    brandId = model.SelectedBrandId,
                    modelId = model.SelectedModelId,
                    sort = model.Sort,
                    page = 0
                });
        }
        private BrowseViewModel PopulateBrowseViewModel(int brandId, int modelId, int sort, int page)
        {

            var viewModel = new BrowseViewModel
            {
                Brands = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },

                CarModels = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },
                SortCriterias = new List<SelectListItem>() {
                     new SelectListItem { Value = "0", Text = "Published" },
                     new SelectListItem { Value = "1", Text = "Price Ascending" },
                     new SelectListItem { Value = "2", Text = "Price Descending" },
                     }
            };
            //if (viewModel.Sort == 0 &&) { viewModel.Sort = -1; }
            if (viewModel.SelectedBrandId == 0 && brandId != 0) { viewModel.SelectedBrandId = brandId; }
            if (viewModel.SelectedModelId == 0 && modelId != 0) { viewModel.SelectedModelId = modelId; }
            if (viewModel.Sort == 0 && sort != 0) { viewModel.Sort = sort; }

            var nPerPage = 5;
            var pageCount = 0;
            var totalCount = this.carService.GetAllCarsCount();
            var reminder = totalCount % nPerPage;
            if (reminder != 0) { pageCount = (totalCount / nPerPage) + 1; }
            else { pageCount = totalCount / nPerPage; }

            var skip = page * nPerPage;
            var take = nPerPage;

            Expression<Func<Car, bool>> filterCriteria = (Car car) => true;
            IList<Car> cars = new List<Car>();

            filterCriteria = brandId == 0 && modelId == 0
                ? null
                : modelId == 0
                      ? (c => c.BrandId == (brandId))
                      : (Expression<Func<Car, bool>>)(c => c.BrandId == (brandId) && c.CarModelId == modelId);

            cars = filterCriteria == null ?
                this.carService.GetCars(skip, take, sort) :
                this.carService.GetCars(skip, take, filterCriteria, sort);

            viewModel.Summaries = cars
            .Select(c => new CarSummaryViewModel(c)
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
            });
            viewModel.NumberOfPages = pageCount;
            viewModel.CurrentPage = page;
            viewModel.Brands
                .AddRange(this.brandService.GetBrands()
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList());

            if (brandId != 0)
            {
                viewModel.CarModels.AddRange(this.modelService.GetAllModelsByBrandId(brandId)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList());
            }
            return viewModel;
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