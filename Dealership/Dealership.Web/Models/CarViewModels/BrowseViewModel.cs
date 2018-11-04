using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models.CarViewModels
{
    public class BrowseViewModel
    {
        public BrowseViewModel()
        {
            Brands = new List<SelectListItem>() { new SelectListItem { Value = "all", Text = "All" } };
            CarModels = new List<SelectListItem>() { new SelectListItem { Value = "all", Text = "All" } };
        }

        public IEnumerable<CarSummaryViewModel> Summaries { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> CarModels { get; set; }
    }
}
