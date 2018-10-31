using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Data.Context;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            var list = this.carService.GetCars("asc");
            return View(list);
        }
    }
}