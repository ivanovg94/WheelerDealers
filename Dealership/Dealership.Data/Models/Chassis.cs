using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Chassis
    {
        private ICollection<Car> _cars;

        public Chassis()
        {
            this._cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte NumberOfDoors { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
            }
        }
    }
}
