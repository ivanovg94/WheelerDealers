using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class Color : Entity
    {
        private ICollection<Car> _cars;

        public Color()
        {
            this._cars = new HashSet<Car>();
        }

        public string Name { get; set; }

        public int ColorTypeId { get; set; }

        public virtual ColorType ColorType { get; set; }

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
