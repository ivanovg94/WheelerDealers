namespace Dealership.Client.Core.Abstract
{
    public interface IWriter
    {
        void WriteLine(string message);

        void Write(string message);
    }
}
