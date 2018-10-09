using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Extra : Entity
    {
        private ICollection<CarsExtras> _carsExtras;

        public Extra()
        {
            this._carsExtras = new HashSet<CarsExtras>();
        }

        public string Name { get; set; }

        public virtual ICollection<CarsExtras> CarsExtras
        {
            get { return _carsExtras; }
            set
            {
                _carsExtras = value;
            }
        }

    }
}
