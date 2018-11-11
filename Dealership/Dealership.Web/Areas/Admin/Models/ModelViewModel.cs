using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Areas.Admin.Models
{
    public class ModelViewModel
    {
        public int BrandId { get; set; }
        public string ModelName { get; set; }
        public IList<SelectListItem> Brands { get; set; }

        public ModelViewModel()
        {

        }
    }
}
