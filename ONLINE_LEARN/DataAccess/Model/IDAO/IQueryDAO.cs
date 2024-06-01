using System.Linq.Expressions;

namespace DataAccess.Model.IDAO
{
    public interface IQueryDAO<T> where T : class
    {
        IQueryable<T> FindAll(params Expression<Func<T, bool>>[] predicate);
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetById(object id);
        Task<int> Count();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }
}
