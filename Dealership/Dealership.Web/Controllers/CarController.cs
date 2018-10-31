using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Data.Context;
using Dealership.Data.Models;
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
            return View(carService);
        }

        public IActionResult Details(int id)
        {
            var car = this.carService.GetCar(id);
            return View(car);
        }
    }
}