using Dealership.Data.Context.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using System;
using System.Collections.Generic;

namespace Dealership.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDealershipContext context;
        private readonly Dictionary<Type, object> repos = new Dictionary<Type, object>();

        public UnitOfWork()
        {
        }

        public UnitOfWork(IDealershipContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
        //TODO: virual?
        public virtual IRepository<T> GetRepository<T>() where T : class, IDeletable
        {
            var repoType = typeof(Repository<T>);

            if (!repos.ContainsKey(repoType))
            {
                var repo = Activator.CreateInstance(repoType, this.context);
                repos[repoType] = repo;
            }
            
            return (IRepository<T>)repos[repoType];
        }
    }
}
