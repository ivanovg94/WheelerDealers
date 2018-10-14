using Dealership.Data.Models.Contracts;

namespace Dealership.Data.Models
{
    public class UserSession : IUserSession
    {
        public virtual User CurrentUser { get; set; }
    }
}
