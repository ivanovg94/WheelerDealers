using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class GearType : Entity
    {
        private ICollection<Gearbox> _gearboxes;

        public GearType()
        {
            this._gearboxes = new HashSet<Gearbox>();
        }

        public string Name { get; set; }

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
