using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Models
{
    public class AddViewModel
    {
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public IList<SelectListItem> Brands { get; set; }
        public string Model { get; set; }
        public string Extra { get; set; }

        public AddViewModel()
        {

        }
    }
}
