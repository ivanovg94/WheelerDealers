using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.DataProcessor.Dto
{
    public class CarDto
    {
        public string BrandName { get; set; }

        public string Model { get; set; }

        public string Chassis { get; set; }

        public byte NDoors { get; set; }

        public short HorsePower { get; set; }

        public short EngineCapacity { get; set; }

        public DateTime ProductionDate { get; set; }

        public decimal Price { get; set; }

        public string Color { get; set; }

        public string ColorType { get; set; }

        public string Fuel { get; set; }

        public string GearBox { get; set; }

        public int NumberOfGears { get; set; }

        public ICollection<string> Extras { get; set; }
    }
}
