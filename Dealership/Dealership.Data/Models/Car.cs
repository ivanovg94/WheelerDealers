using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dealership.Data.Models
{
    public class Car : Entity
    {
        private ICollection<CarsExtras> _carsExtras;

        public Car()
        {
            this._carsExtras = new HashSet<CarsExtras>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Model { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public short HorsePower { get; set; }

        [Required]
        [Range(1, 100000)]
        public short EngineCapacity { get; set; }

        public bool IsSold { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ProductionDate { get; set; }

        public int BrandId { get; set; }   
        public virtual Brand Brand { get; set; }

        public int BodyTypeId { get; set; }
        public virtual BodyType BodyType { get; set; }

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
