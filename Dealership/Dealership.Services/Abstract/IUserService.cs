using Dealership.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
    }
}
