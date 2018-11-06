using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Models
{
    public class UsersViewModel
    {
        public IEnumerable<string> Options { get; set; }
        public string Value { get; set; }
        public int Selection { get; set; }
        public string SelectedAnswer { get; set; }

        public UsersViewModel()
        {

        }
        public UsersViewModel(IEnumerable<string> options)
        {
            this.Options = options;
        }
    }
}
