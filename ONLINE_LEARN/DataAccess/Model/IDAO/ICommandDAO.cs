namespace DataAccess.Model.IDAO
{
    public interface ICommandDAO<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Save();
    }
}
