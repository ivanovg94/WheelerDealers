using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Web.Areas.Admin.Models
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public int BrandId { get; set; }

        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        public string Brand { get; set; }

        public IList<SelectListItem> Brands { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Extra { get; set; }
        public string StatusMessage { get; set; }
    }
}