using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models
{
    public class EditCarViewModel
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public Color Color { get; set; }

        public int EngineCapacity { get; set; }

        public int Price { get; set; }

        public DateTime ProductionDate { get; set; }

        public EditCarViewModel()
        {

        }
    }
}
