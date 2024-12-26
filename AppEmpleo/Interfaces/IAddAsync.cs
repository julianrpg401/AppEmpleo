namespace AppEmpleo.Interfaces
{
    public interface IAddAsync<T> where T : class
    {
        Task AddAsync(T item);
    }
}
