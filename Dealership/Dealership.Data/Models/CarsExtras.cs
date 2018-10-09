using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
   public class CarsExtras
    {
        public int CarId { get; set; }

        public int ExtraId { get; set; }

        public virtual Car Car { get; set; }

        public virtual Extra Extra { get; set; }
    }
}
