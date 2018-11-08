using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Models.CarViewModels
{
    public class BrowseViewModel
    {
        public BrowseViewModel()
        {
            Brands = new List<SelectListItem>() { new SelectListItem { Value = "-1", Text = "All" } };
            CarModels = new List<SelectListItem>() { new SelectListItem { Value = "-1", Text = "All" } };
            SortCriterias = new List<SelectListItem>() {
                new SelectListItem { Value = "-1", Text = "Published" },
                new SelectListItem { Value = "1", Text = "Price Ascending" },
                new SelectListItem { Value = "-1", Text = "Price Descending" },
            };
        }

        public IEnumerable<CarSummaryViewModel> Summaries { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public int SelectedBrandId { get; set; }
        public int SelectedModelId { get; set; }

        public string SelectedSort { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> CarModels { get; set; }
        public List<SelectListItem> SortCriterias { get; set; }
    }
}
