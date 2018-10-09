using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class ColorType : Entity
    {
        private ICollection<Color> _colors;

        public ColorType()
        {
            this._colors = new HashSet<Color>();
        }

        public string Name { get; set; }

        public virtual ICollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
            }
        }
    }
}
