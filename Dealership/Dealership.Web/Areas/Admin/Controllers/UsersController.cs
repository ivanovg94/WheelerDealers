using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class UsersController : Controller
    {
        public IUserService UserService { get; }

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }
        
        public IActionResult Index()
        {
            //this.UserService.GetUsers();
            return View();
        }
    }
}