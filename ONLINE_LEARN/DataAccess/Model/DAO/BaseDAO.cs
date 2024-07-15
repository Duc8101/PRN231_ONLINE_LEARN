using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class BaseDAO
    {
        internal readonly MyDbContext _context;
        public BaseDAO(MyDbContext context)
        {
            _context = context;
        }
    }
}
