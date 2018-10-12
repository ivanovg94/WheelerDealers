using Dealership.Client.Contracts.Abstract;

namespace Dealership.Client.Core.Abstract
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string args);
    }
}