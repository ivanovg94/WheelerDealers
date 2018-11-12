using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Web.Areas.Admin.Models
{
    public class AddBrandViewModel
    {
        public AddBrandViewModel()
        {
        }

        public int BrandId { get; set; }

        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        [Remote(action: "DoesBrandExist", controller: "Admin", areaName: "Admin")]
        public string Brand { get; set; }

        public string StatusMessage { get; set; }
    }
}
