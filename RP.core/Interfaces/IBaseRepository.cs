
using System.Linq.Expressions;


namespace TaskManagement.core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    { 
        T find(Expression<Func<T,bool>> x, string[] includes1 = null, string[] includes2 = null);

        IEnumerable<T> GetAll(string[] includes1 = null, string[] includes2 = null);

        IEnumerable<T> GetAllbyExpression(Expression<Func<T, bool>> x, string[] includes1 = null, string[] includes2 = null);

        IEnumerable< T> GetAllbyExpression(Expression<Func<T, bool>> x, Expression<Func<T,
                                            bool>> y, string[] includes1 = null, string[] includes2 = null);
        T add(T item);
        void update(T item);
        void delete(T item);
    }
}
