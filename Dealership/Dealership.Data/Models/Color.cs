using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Color
    {
        private ICollection<Car> _cars;

        public Color()
        {
            this._cars = new HashSet<Car>();
        }

        public int Id { get; set; }
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
