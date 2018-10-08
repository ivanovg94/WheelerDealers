using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dealership.Data.Models
{
    public class Gearbox
    {
        private ICollection<Car> _cars;

        public Gearbox()
        {
            this._cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public int GearTypeId { get; set; }
        public byte NumberOfGears { get; set; }

        public virtual GearType GearType { get; set; }
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
