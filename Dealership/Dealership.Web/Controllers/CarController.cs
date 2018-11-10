using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public CarController(ICarService carService, IEditCarService editCarService, IBrandService brandService,
            IBodyTypeService bodyTypeService, IColorTypeService colorTypeService,
            IFuelTypeService fuelTypeService, IGearTypeService gearTypeService,
            IModelService modelService, IColorService colorService, IUserService userService, UserManager<User> userManager)
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
            this.userService = userService;
            this.userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            var list = this.carService.GetCarsAsync(0, 99);

            return View(list);

        }

        [HttpGet]
        public async Task<IActionResult> Browse(int page)
        {
            var nPerPage = 5;
            var pageCount = 0;
            var totalCount = this.carService.GetCarsCount();
            var reminder = totalCount % nPerPage;

            if (reminder != 0)
            {
                pageCount = (totalCount / nPerPage) + 1;
            }
            else
            {
                pageCount = totalCount / nPerPage;
            }

            //Expression<Func<Car, bool>> searchCriteria = (Car car) => true;
            //searchCriteria = x => x.Brand.Name.Contains("");

            var summarys = await this.carService.GetCarsAsync(page * nPerPage, nPerPage);
            var model = new BrowseViewModel()
            {
                Summaries = summarys
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
                    Mileage = $"{c.Mileage} miles",

                }),
                Pages = page,
                CurrentPage = pageCount
            };

            model.Brands
                .AddRange(this.brandService.GetBrands()
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList());

            return this.View(model);
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await this.carService.GetCarAsync(id);
            var user = this.userManager.GetUserAsync(HttpContext.User).Result;
            CarViewModel model;
            if (user == null)
            {
                model = new CarViewModel(car)
                {
                    StatusMessage = this.StatusMessage
                };
            }
            else
            {
                model = new CarViewModel(car)
                {
                    IsFavorite = userService.IsCarFavorite(id, user),
                    StatusMessage = this.StatusMessage
                };
            }

            return this.View(model);
        }
    }
}