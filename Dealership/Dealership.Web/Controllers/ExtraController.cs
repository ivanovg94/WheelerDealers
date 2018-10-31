using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Web.Controllers
{
    public class ExtraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}