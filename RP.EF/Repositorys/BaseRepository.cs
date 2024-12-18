using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.core.Interfaces;

namespace TaskManagement.EF.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _Context;
        public BaseRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public T find(Expression<Func<T, bool>> x, string[] includes1 = null, string[] includes2 = null) 
        {
            IQueryable<T> q = _Context.Set<T>();
            if (includes1 != null) {
                foreach (var include in includes1)
                {
                    q = q.Include(include);
                } 
            }
            if (includes2 != null)
            {
                foreach (var include in includes2)
                {
                    q = q.Include(include);
                }
            }

            return q.SingleOrDefault(x);
        }

        public IEnumerable<T> GetAll(string[] includes1 = null, string[] includes2 = null)
        {
            IQueryable<T> q = _Context.Set<T>();
            if (includes1 != null)
            {
                foreach (var include in includes1)
                {
                    q = q.Include(include);
                }
            }
            if (includes2 != null)
            {
                foreach (var include in includes2)
                {
                    q = q.Include(include);
                }
            }
            return q.ToList();
        }

        public IEnumerable<T> GetAllbyExpression(Expression<Func<T, bool>> x, string[] includes1 = null, string[] includes2 = null)
        {
            IQueryable<T> q = _Context.Set<T>();
            if (includes1 != null)
            {
                foreach (var include in includes1)
                {
                    q = q.Include(include);
                }
            }
            if (includes2 != null)
            {
                foreach (var include in includes2)
                {
                    q = q.Include(include);
                }
            }
            return q.Where(x).ToList();
        }
       public IEnumerable<T> GetAllbyExpression(Expression<Func<T, bool>> x, Expression<Func<T, bool>> y, string[] includes1 = null, string[] includes2 = null)
        { 
        IQueryable<T> q = _Context.Set<T>();
            if (includes1 != null)
            {
                foreach (var include in includes1)
                {
                    q = q.Include(include);
                }
            }
            if (includes2 != null)
            {
                foreach (var include in includes2)
                {
                    q = q.Include(include);
                }
            }
            
            return q.Where(y).Where(x).ToList(); ;
        }

        public T add(T item) 
        {
            _Context.Set<T>().Add(item);
            return item;
        }

        public void update(T item) 
        { 
            _Context.Set<T>().Attach(item);
            _Context.Entry(item).State = EntityState.Modified;
       
        }

        public void delete(T item)
        {
            _Context.Set<T>().Remove(item);
        }

    }

}


