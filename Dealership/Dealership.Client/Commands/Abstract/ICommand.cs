namespace Dealership.Client.Contracts.Abstract
{
    public interface ICommand
    {
        string Execute(string[] parameters);
    }
}
