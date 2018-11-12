using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Models
{
    public class AddExtraViewModel
    {
        public AddExtraViewModel()
        {
        }

        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        [Remote(action: "DoesExtraExist", controller: "Admin", areaName: "Admin")]
        public string Extra { get; set; }

        public string StatusMessage { get; set; }
    }
}
