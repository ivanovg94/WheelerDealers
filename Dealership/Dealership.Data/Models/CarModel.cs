using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dealership.Data.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
