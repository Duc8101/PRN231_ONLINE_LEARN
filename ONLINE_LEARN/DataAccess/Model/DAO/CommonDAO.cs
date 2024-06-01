using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Model.DAO
{
    public class CommonDAO<T> : ICommonDAO<T> where T : class
    {
        private readonly MyDbContext _context;
        public CommonDAO(MyDbContext context) 
        {
            _context = context;   
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);  
        }

        public async Task<int> Count()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, bool>>[] predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            if(predicate != null)
            {
                foreach(var item in predicate)
                {
                    query = query.Where(item);
                }
            }
            return query;
        }

        public async Task<T?> Get(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T?> GetById(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
