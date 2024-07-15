using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOCategory : BaseDAO
    {
        public DAOCategory(MyDbContext context) : base(context)
        {
        }

        public List<Category> getListCategory()
        {
            return _context.Categories.ToList();
        }
    }
}
