using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
