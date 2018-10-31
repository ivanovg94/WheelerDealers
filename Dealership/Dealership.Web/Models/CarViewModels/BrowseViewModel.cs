using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models.CarViewModels
{
    public class BrowseViewModel
    {
        public IEnumerable<CarSummaryViewModel> Summaries { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
