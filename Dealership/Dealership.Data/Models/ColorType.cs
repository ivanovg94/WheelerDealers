using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class ColorType
    {
        private ICollection<Color> _colors;

        public ColorType()
        {
            this._colors = new HashSet<Color>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

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
