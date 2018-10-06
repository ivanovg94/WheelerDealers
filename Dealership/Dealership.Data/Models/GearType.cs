using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class GearType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Gearbox> Gearboxes { get; set; }
    }
}
