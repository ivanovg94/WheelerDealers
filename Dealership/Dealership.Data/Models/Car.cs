using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public short HorsePower { get; set; }
        public short EngineCapacity { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ChasisId { get; set; }
        public Chasis Chasis { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public int GearBoxId { get; set; }
        public GearBox GearBox { get; set; }
    }
}
