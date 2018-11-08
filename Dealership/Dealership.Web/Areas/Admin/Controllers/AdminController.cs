using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Data.Models;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Dealership.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IModelService modelService;

        public IUserService UserService { get; }
        public ICarService CarService { get; }
        public IBrandService BrandService { get; set; }
        public IExtraService ExtraService { get; set; }

        public AdminController(IModelService modelService, IUserService userService, ICarService carService, IBrandService brandService, IExtraService extraService)
        {
            this.modelService = modelService;
            UserService = userService;
            CarService = carService;
            BrandService = brandService;
            ExtraService = extraService;
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

        public IActionResult AddBrand(string brand)
        {
            var newBrand = this.BrandService.Create(brand);
            this.BrandService.Add(newBrand);
            return Redirect("~/Index");
        }

        public IActionResult AddModel(string model)
        {
            return View();
        }

        public IActionResult ManageCars()
        {
            var carViewModels = CarService.GetCars(false, "asc").Select(c => new CarViewModel(c)).ToList();

            return View(carViewModels);
        }

        public IActionResult AddExtra(string extra)
        {
            var newExtra = this.ExtraService.CreateExtra(extra);
            this.ExtraService.AddExtra(newExtra);
            return Redirect("~/Index");
        }

        public IActionResult Delete(string confirm, int id)
        {
            return View();
        }

    }
}