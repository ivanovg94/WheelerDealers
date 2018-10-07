using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class GearType
    {
        private ICollection<Gearbox> _gearboxes;

        public GearType()
        {
            this._gearboxes = new HashSet<Gearbox>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Gearbox> Gearboxes
        {
            get { return _gearboxes; }
            set
            {
                _gearboxes = value;
            }
        }
    }
}
