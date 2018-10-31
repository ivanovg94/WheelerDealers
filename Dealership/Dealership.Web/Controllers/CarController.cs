using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Services.Abstract;
using Dealership.Web.Models.CarViewModels;
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
    }
}