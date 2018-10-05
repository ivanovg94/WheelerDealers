using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
   public class Chassis
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int NumberOfDoors { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
