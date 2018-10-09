using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class FuelType : Entity
    {
        private ICollection<Car> _cars;

        public FuelType()
        {
            this._cars = new HashSet<Car>();
        }

        public string Name { get; set; }

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
