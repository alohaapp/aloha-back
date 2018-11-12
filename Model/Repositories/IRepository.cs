using System.Collections.Generic;

namespace Aloha.Model.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(int id);

        IEnumerable<T> List();

        void Add(T entity);

        void Remove(int id);
    }
}