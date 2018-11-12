using System.Collections.Generic;
using System.Linq;
using Aloha.Models.Contexts;

namespace Aloha.Model.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly AlohaContext dbContext;

        public Repository(AlohaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return dbContext.Set<T>().AsEnumerable();
        }

        public virtual void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public virtual void Remove(int id)
        {
            T entity = GetById(id);
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }
    }
}