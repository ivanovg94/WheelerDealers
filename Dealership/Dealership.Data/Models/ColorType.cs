using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class ColorType : Entity
    {
        private ICollection<Color> _colors;

        public ColorType()
        {
            this._colors = new HashSet<Color>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
            }
        }
    }
}
