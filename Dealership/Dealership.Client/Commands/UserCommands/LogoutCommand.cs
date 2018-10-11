using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;

namespace Dealership.Client.Commands.UserCommands
{
    public class LogoutCommand : PrimeCommand
    {
        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            string username = UserService.GetCurrentUser()?.Username;

            UserService.Logout();

            return $"User {username} successfully logged out!";
        }
    }
}
