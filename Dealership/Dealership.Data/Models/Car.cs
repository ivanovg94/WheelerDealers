using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Car
    {
        private ICollection<CarsExtras> _carsExtras;

        public Car()
        {
            this._carsExtras = new HashSet<CarsExtras>();
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public short HorsePower { get; set; }
        public short EngineCapacity { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int ChasisId { get; set; }
        public virtual Chassis Chasis { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int FuelTypeId { get; set; }
        public virtual FuelType FuelType { get; set; }
        public int GearBoxId { get; set; }
        public virtual Gearbox GearBox { get; set; }

        public virtual ICollection<CarsExtras> CarsExtras
        {
            get { return _carsExtras; }
            set
            {
                _carsExtras = value;
            }
        }
    }
}
