using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IDeletable;

        int SaveChanges();
    }
}
