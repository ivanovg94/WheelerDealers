using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class Extra
    {
        private ICollection<CarsExtras> _carsExtras;

        public Extra()
        {
            this._carsExtras = new HashSet<CarsExtras>();
        }

        public int Id { get; set; }
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
