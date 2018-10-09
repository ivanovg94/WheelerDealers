﻿using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class Chassis : Entity
    {
        private ICollection<Car> _cars;

        public Chassis()
        {
            this._cars = new HashSet<Car>();
        }

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
