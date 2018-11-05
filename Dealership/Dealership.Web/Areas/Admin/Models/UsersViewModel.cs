using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Models
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public UsersViewModel(IEnumerable<User> users)
        {
            this.Users = users;
        }
    }
}
