using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Dealership.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IModelService modelService;
        private readonly IUserService userService;
        private readonly ICarService carService;
        private readonly IBrandService brandService;
        private readonly IExtraService extraService;

        public AdminController(IModelService modelService, IUserService userService, ICarService carService, IBrandService brandService, IExtraService extraService)
        {
            this.modelService = modelService;
            this.userService = userService;
            this.carService = carService;
            this.brandService = brandService;
            this.extraService = extraService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UsersViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(UsersViewModel model)
        {
            if (model.Value != null)
            {

                var meths = this.GetType().GetMethods().Where(m => m.Name.Contains("Add"));
                foreach (var met in meths)
                {
                    if (met.Name.ToLower().Contains(model.SelectedAnswer.ToLower()))
                    {
                        met.Invoke(this, new object[] { model.Value });
                        break;
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddBrandsAndExtras()
        {
            var model = new UsersViewModel();
            return View(model);
        }

        public IActionResult AddBrand(string brand)
        {
            var newBrand = this.brandService.Create(brand);
            this.brandService.Add(newBrand);
            return Redirect("~/Index");
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
        public IActionResult AddModel(int brandId, string modelName)
        {

            //validation todo
            this.modelService.Add(brandId, modelName);

            return View();
        }

        public IActionResult ManageCars()
        {
            var carViewModels = carService.GetCars(0, int.MaxValue).Select(c => new CarViewModel(c)).ToList();
            return View(carViewModels);
        }

        public IActionResult AddExtra(string extra)
        {
            var newExtra = this.extraService.CreateExtra(extra);
            this.extraService.AddExtra(newExtra);
            return Redirect("~/Index");
        }

        public IActionResult Delete(int id)
        {
            this.carService.RemoveCar(id);
            return View();
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