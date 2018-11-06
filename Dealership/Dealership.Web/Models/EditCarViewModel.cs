using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Models
{
    public class EditCarViewModel
    {
        public CarViewModel Car { get; set; }

        public List<SelectListItem> Brands { get; set; }

        public List<SelectListItem> CarModels { get; set; }

        public List<SelectListItem> GearTypes { get; set; }

        public List<SelectListItem> NumberOfGears { get; set; }

        public List<SelectListItem> BodyTypes { get; set; }

        public List<SelectListItem> ColorTypes { get; set; }

        public List<SelectListItem> FuelTypes { get; set; }
    }
}
