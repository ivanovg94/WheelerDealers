using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Dealership.Data.Models
{
    public class User : Entity
    {
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public ICollection<Car> FavoriteCars { get; set; }
    }
}
