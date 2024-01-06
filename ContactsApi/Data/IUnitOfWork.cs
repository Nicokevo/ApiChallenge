namespace ContactsApi.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
