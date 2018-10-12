using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class LoginCommand : PrimeCommand
    {
        public LoginCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            string username = parameters[0];
            string password = parameters[1];

            if (base.UserSession.CurrentUser != null)
            {
                throw new InvalidOperationException("There is logged in user.");
            }

            var user = this.UserService.GetUserByCredentials(username, password);            
            base.UserSession.CurrentUser = user;

            return $"User {username} successfully logged in!";
        }
    }
}
