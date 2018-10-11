using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class GearType : Entity
    {
        private ICollection<Gearbox> _gearboxes;

        public GearType()
        {
            this._gearboxes = new HashSet<Gearbox>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
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
