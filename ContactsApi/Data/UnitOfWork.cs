using System.Threading.Tasks;

namespace ContactsApi.Data
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactsApiContext _context;

        public UnitOfWork(ContactsApiContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
