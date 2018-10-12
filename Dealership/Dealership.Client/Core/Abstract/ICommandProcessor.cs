namespace Dealership.Client.Core.Abstract
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string args);
    }
}