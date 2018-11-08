using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Data.Models;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Dealership.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        [TempData]
        public string StatusMessage { get; set; }

        public AdminController(IFuelTypeService fuelTypeService, IColorTypeService colorTypeService,
            IBodyTypeService bodyTypeService,
            IGearTypeService gearTypeService, IModelService modelService, IUserService userService, ICarService carService, IBrandService brandService, IExtraService extraService)
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
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UsersViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddExtra(AddViewModel model)
        {
            string extra = model.Extra;
            var newExtra = this.extraService.CreateExtra(extra);
            this.extraService.AddExtra(newExtra);
            return Redirect("~/Browse");

        }
        [HttpGet]
        public IActionResult AddBrandsExtrasModels()
        {
            var model = new AddViewModel()
            {
                Brands = this.brandService.GetBrands()
               .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
            return View(model);
        }

        public IActionResult AddBrand(AddViewModel model)
        {
            var brand = model.Brand;
            var newBrand = this.brandService.Create(brand);
            this.brandService.Add(newBrand);
            return Redirect("~/Browse");
        }
        [HttpGet]
        public IActionResult AddModel()
        {
            var model = new ModelViewModel()
            {
                Brands = this.brandService.GetBrands()
               .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddModel(string model, int brandId)
        {

            //validation todo
            this.modelService.Add(brandId, model);

            return Redirect("~/admin/Admin/AddBrandsExtrasModels");
        }
        [HttpGet]
        public IActionResult ManageCars()
        {
            //var carViewModels = this.carService.GetCars(0, int.MaxValue).Select(c => new CarViewModel(c)).ToList();
            //return View(carViewModels);

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
        [Authorize]
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

        public IActionResult Delete(int id)
        {
            this.carService.RemoveCar(id);
            return View();
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