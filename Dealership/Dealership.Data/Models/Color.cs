using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColorTypeId { get; set; }
        public ColorType ColorType { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
