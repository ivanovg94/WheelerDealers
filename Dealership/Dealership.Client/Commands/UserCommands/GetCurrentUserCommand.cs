using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class GetCurrentUserCommand : PrimeCommand
    {
        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            var user = this.UserService.GetCurrentUser();

            return $"Current user: {user.Username} {user.Password}";
        }
    }
}
