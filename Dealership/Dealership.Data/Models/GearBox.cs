using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Gearbox
    {
        public int Id { get; set; }
        public int GearTypeId { get; set; }
        public byte NumberOfGears { get; set; }

        public GearType GearType { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
