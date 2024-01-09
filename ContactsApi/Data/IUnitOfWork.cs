using ContactsApi.Repositories;
using System;
using System.Threading.Tasks;

namespace ContactsApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository ContactRepository { get; }
        Task SaveAsync();
    }
}
