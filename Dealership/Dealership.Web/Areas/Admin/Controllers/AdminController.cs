using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Data.Models;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdminController : Controller
    {
        public IBrandService BrandService { get; set; }
        public IExtraService ExtraService { get; set; }

        public AdminController(IBrandService brandService, IExtraService extraService)
        {
            BrandService = brandService;
            ExtraService = extraService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UsersViewModel(new string[] { "Brand", "Model", "Extra" });
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(UsersViewModel model)
        {
            //do work
            var meths = this.GetType().GetMethods().Where(m => m.Name.Contains("Add"));
            foreach (var met in meths)
            {
                if (met.Name.Contains(model.SelectedAnswer))
                {
                    met.Invoke(this, new object[] { model.Value });
                    break;
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

        public IActionResult AddExtra(string extra)
        {
            var newExtra = this.ExtraService.CreateExtra(extra);
            this.ExtraService.AddExtra(newExtra);
            return Redirect("~/Index");
        }


    }
}