using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class RegisterCommand : PrimeCommand
    {
        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            string username = parameters[0];
            string password = parameters[1];
            string confirmPassword = parameters[2];
            string email = parameters[2];

            var user = this.UserService.RegisterUser(username, password, confirmPassword, email);

            return $"User {username} was registered succesfully";
        }
    }
}
