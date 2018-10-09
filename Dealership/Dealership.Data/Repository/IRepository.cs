using Dealership.Data.Models.Contracts;
using System.Linq;

namespace Dealership.Data.Repository
{
    public interface IRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All();

        IQueryable<T> AllAndDeleted();

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        void Save();
    }
}
