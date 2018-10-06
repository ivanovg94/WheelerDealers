using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
   public class Chassis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte NumberOfDoors { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
