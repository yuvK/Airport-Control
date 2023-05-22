namespace Airpoot.API.DAL
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        T? Get(Guid Id);
        IQueryable<T> GetAll();
        IQueryable<T> GetLastFew(int amount);
    }
}
