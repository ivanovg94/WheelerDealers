using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Car : Entity, ICar
    {
        private ICollection<CarsExtras> carsExtras;
        private ICollection<UsersCars> usersCars;
        private ICollection<Image> images;

        public Car()
        {
            this.carsExtras = new HashSet<CarsExtras>();
            this.usersCars = new HashSet<UsersCars>();
            this.images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Model { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
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
        public Brand Brand { get; set; }

        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        public int GearBoxId { get; set; }
        public Gearbox GearBox { get; set; }

        public ICollection<Image> Images
        {
            get { return this.images; }
            set
            {
                this.images = value;
            }
        }


        public ICollection<CarsExtras> CarsExtras
        {
            get { return this.carsExtras; }
            set
            {
                this.carsExtras = value;
            }
        }

        public ICollection<UsersCars> UsersCars
        {
            get { return this.usersCars; }
            set
            {
                this.usersCars = value;
            }
        }
    }
}
