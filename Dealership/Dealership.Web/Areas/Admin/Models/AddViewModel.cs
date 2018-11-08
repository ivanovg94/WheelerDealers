using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Areas.Admin.Models
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public int BrandId { get; set; }
        public string Brand { get; set; }
        public IList<SelectListItem> Brands { get; set; }
        public string Model { get; set; }
        public string Extra { get; set; }
        public string StatusMessage { get; set; }
    }
}
