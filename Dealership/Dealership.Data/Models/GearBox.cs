using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Gearbox : Entity
    {
        private ICollection<Car> _cars;

        public Gearbox()
        {
            this._cars = new HashSet<Car>();
        }

        public int GearTypeId { get; set; }

        [Range(1, 10)]
        public byte NumberOfGears { get; set; }

        public GearType GearType { get; set; }

        public ICollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
            }
        }
    }
}
