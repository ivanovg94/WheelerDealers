using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.ViewModels
{
    public class CarVM
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string Chassis { get; set; }
        public byte NDoors { get; set; }
        public short EngineCap { get; set; }
        public short HorsePower { get; set; }
        public DateTime ProductionDate { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ColorType { get; set; }
        public string Fuel { get; set; }
        public string Gearbox { get; set; }
        public byte NumberOfGears { get; set; }
        public IList<string> Extras { get; set; }

        public override string ToString()
        {
            return $"Id:{this.Id} {this.BrandName} {this.Model}, Engine: {this.EngineCap}cc {this.Fuel} {this.HorsePower}hp, Body type {this.NumberOfGears} door {this.Chassis}, Prod.: {this.ProductionDate.ToShortDateString()}, Price: {this.Price}, Color: {this.Color} {this.ColorType} Transmission: {this.NumberOfGears} step {this.Gearbox} \r\nExtras: {string.Join(", ",this.Extras)}\r\n";
        }
    }
}
