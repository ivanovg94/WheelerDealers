using System;
namespace Dealership.Client.Core.Abstract
{
    public interface IExceptionLogging
    {
        void SendErrorToText(Exception ex);
    }
}