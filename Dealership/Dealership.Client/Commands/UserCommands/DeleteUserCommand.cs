using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.UserCommands
{
    public class DeleteUserCommand : Command
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserSession userSession, IUserService userService) : base(userSession)
        {
            this.userService = userService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Invalid parameters");
            }
            string username = parameters[0];
            string password = parameters[1];


            var user = this.userService.DeleteUser(username, password);
            base.UserSession.CurrentUser = null;

            return $"User {username} successfully deleted!";
        }
    }
}
