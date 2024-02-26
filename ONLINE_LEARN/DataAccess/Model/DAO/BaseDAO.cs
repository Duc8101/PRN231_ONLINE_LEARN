namespace DataAccess.Model.DAO
{
    public class BaseDAO
    {
        internal readonly MyDbContext context = new MyDbContext();
    }
}
