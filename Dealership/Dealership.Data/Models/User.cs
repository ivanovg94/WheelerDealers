﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class User : Entity
    {
        private ICollection<UsersCars> userCars;

        public User()
        {
            this.userCars = new HashSet<UsersCars>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [MaxLength(25)]
        public UserType UserType { get; set; }

        public ICollection<UsersCars> UsersCars
        {
            get { return this.userCars; }
            set
            {
                this.userCars = value;
            }
        }
    }
}
