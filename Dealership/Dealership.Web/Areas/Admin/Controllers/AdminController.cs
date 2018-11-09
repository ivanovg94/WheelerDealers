using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Dealership.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBrandService brandService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IColorTypeService colorTypeService;
        private readonly IBodyTypeService bodyTypeService;
        private readonly IGearTypeService gearTypeService;
        private readonly IModelService modelService;
        private readonly ICarService carService;
        private readonly IUserService userService;
        private readonly IExtraService extraService;
        private readonly IColorService colorService;

        public AdminController(IFuelTypeService fuelTypeService, IColorTypeService colorTypeService,
            IBodyTypeService bodyTypeService,
            IGearTypeService gearTypeService, IModelService modelService, IUserService userService, ICarService carService, IBrandService brandService, IExtraService extraService, IColorService colorService)
        {
            this.brandService = brandService;
            this.fuelTypeService = fuelTypeService;
            this.colorTypeService = colorTypeService;
            this.bodyTypeService = bodyTypeService;
            this.gearTypeService = gearTypeService;
            this.modelService = modelService;
            this.userService = userService;
            this.carService = carService;
            this.extraService = extraService;
            this.colorService = colorService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UsersViewModel();
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddFeatures()
        {
            var model = new AddViewModel()
            {
                Brands = this.brandService.GetBrands()
               .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                StatusMessage = this.StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExtra(AddViewModel model)
        {
            string extra = model.Extra;
            var newExtra = this.extraService.CreateExtra(extra);
            this.extraService.AddExtra(newExtra);
            this.StatusMessage = "Extra added successfully!";

            return RedirectToAction("AddFeatures");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBrand(AddViewModel model)
        {
            var brand = model.Brand;
            var newBrand = this.brandService.Create(brand);
            this.brandService.Add(newBrand);
            this.StatusMessage = "Brand added successfully!";

            return RedirectToAction("AddFeatures");
        }

        [HttpGet]
        public IActionResult AddModel()
        {
            var model = new ModelViewModel()
            {
                Brands = this.brandService.GetBrands()
               .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
            return RedirectToAction("AddFeatures");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddModel(string model, int brandId)
        {
            //validation todo
            this.modelService.Add(brandId, model);
            this.StatusMessage = "Model added successfully!";

            return RedirectToAction("AddFeatures");
        }

        [HttpGet]
        public IActionResult ManageCars()
        {
            return RedirectToAction("Browse", "Car", new { area = "" });
        }

        [HttpGet]
        public IActionResult CreateCar()
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
        [ValidateAntiForgeryToken]
        public IActionResult CreateCar(EditCarViewModel model)
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
                        return this.RedirectToAction(nameof(CreateCar));
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

            return RedirectToAction("Details", "Car", new { area = "", id = car.Id });
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
        public IActionResult Delete(bool confirm, int id)
        {
            if (confirm)
            {
                var removedCar = carService.RemoveCar(id);
            }
            return RedirectToAction("Browse", "Car", new { area = "" });

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
        //public IActionResult ExportJson()
        //{
        //    var cars = carService.GetCars();

        //    var result = cars.Select(c => new CarViewModel
        //    {
        //        Id = c.Id,
        //        Brand = c.Brand.Name,
        //        CarModel = c.CarModel.Name,
        //        EngineCapacity = c.EngineCapacity,
        //        HorsePower = c.HorsePower,
        //        ProductionDate = c.ProductionDate,
        //        Price = c.Price,
        //        BodyType = c.BodyType.Name,
        //        Color = c.Color.Name,
        //        ColorType = c.Color.ColorType.Name,
        //        FuelType = c.FuelType.Name,
        //        GearBoxType = c.GearBox.GearType.Name,
        //        NumberOfGears = c.GearBox.NumberOfGears,
        //        CarsExtras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
        //    }).ToList();

        //    var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        //}
    }
}