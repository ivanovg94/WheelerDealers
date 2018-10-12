using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models
{
    public class UserSession : IUserSession
    {
        public User CurrentUser { get; set; }
    }
}
