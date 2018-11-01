using Dealership.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Web.Models
{
    public class CarViewModel
    {
        public CarViewModel()
        {
        }

        public CarViewModel(Car car)
        {
            this.CarModel = car.Model;
            this.HorsePower = car.HorsePower;
            this.EngineCapacity = car.EngineCapacity;
            this.Price = car.Price;
            this.BodyType = car.BodyType.Name;
            this.Brand = car.Brand.Name;
            this.Color = car.Color.Name;
            this.ColorType = car.Color.ColorType.Name;
            this.ProductionDate = car.ProductionDate;
            this.GearBoxType = car.GearBox.GearType.Name;
            this.NumberOfGears = car.GearBox.NumberOfGears;
            this.FuelType = car.FuelType.Name;
            this.ImageUrl = car.ImageName;
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string CarModel { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public short HorsePower { get; set; }

        [Required]
        [Range(1, 100000)]
        public short EngineCapacity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        public string Brand { get; set; }

        public string BodyType { get; set; }

        public string Color { get; set; }

        public string ColorType { get; set; }

        public string FuelType { get; set; }

        public string GearBoxType { get; set; }

        public int NumberOfGears { get; set; }

        public ICollection<string> CarsExtras { get; set; }

        public List<SelectListItem> GearTypes { get; set; }

        public List<SelectListItem> BodyTypes { get; set; }

        public List<SelectListItem> ColorTypes { get; set; }

        public List<SelectListItem> FuelTypes { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

    }
}
