using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Brand
    {
        private ICollection<Car> _cars;

        public Brand()
        {
            this._cars = new HashSet<Car>();
        }

        public int Id { get; set; }
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
