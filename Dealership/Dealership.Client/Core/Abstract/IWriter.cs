namespace Dealership.Client.Core.Abstract
{
    public interface IWriter
    {
        //void Clean();
        //void PrintAdminCommands();
        //void PrintHeader();
        //void PrintLoginCommands();
        //void PrintUserCommands();
        void Write(string message);
        void WriteLine(string message);

        void PrintCommands();
    }
}
