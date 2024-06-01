namespace DataAccess.Model.IDAO
{
    public interface ICommonDAO<T> : IQueryDAO<T>, ICommandDAO<T> where T : class
    {

    }
}
