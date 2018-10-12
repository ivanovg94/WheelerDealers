using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dealership.Client.Commands.UserCommands
{
    public class RegisterCommand : PrimeCommand
    {
        public RegisterCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            if (base.UserSession.CurrentUser != null)
            {
                throw new InvalidOperationException("There is logged in user. Logout first to register new user!");
            }

            if (parameters.Length != 4)
            {
                throw new ArgumentException("Invalid parameters");
            }

            string username = parameters[0];
            string password = parameters[1];
            string confirmPassword = parameters[2];
            string email = parameters[3];

            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            Match match = regex.Match(email);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid email address.");
            }

            var user = this.UserService.RegisterUser(username, password, confirmPassword, email);
            base.UserSession.CurrentUser = user;

            return $"User {username} was registered succesfully";
        }
    }
}
