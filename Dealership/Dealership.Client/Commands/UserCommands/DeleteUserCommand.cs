using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class DeleteUserCommand : Command
    {
        public DeleteUserCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            string username = parameters[0];
            string password = parameters[1];

            if (parameters.Length != 2)
            {
                throw new ArgumentException("Invalid parameters");
            }

            var user = this.UserService.DeleteUser(username, password);
            base.UserSession.CurrentUser = null;

            return $"User {username} successfully deleted!";
        }
    }
}
