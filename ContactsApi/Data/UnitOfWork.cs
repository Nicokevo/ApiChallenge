using ContactsApi.Repositories;
using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;

namespace ContactsApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactsApiContext _context;
        private readonly IDbContextScopeFactory _contextScopeFactory;
        public IContactRepository ContactRepository { get; }

        public UnitOfWork(IDbContextScopeFactory contextScopeFactory, ContactsApiContext context, IContactRepository contactRepository)
        {
            _contextScopeFactory = contextScopeFactory;
            _context = context;
            ContactRepository = contactRepository;
        }

        public async Task SaveAsync()
        {
            using (var contextScope = _contextScopeFactory.Create())
            {
                await _context.SaveChangesAsync();
                contextScope.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
