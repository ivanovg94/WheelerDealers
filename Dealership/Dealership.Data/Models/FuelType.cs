using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
   public class FuelType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
