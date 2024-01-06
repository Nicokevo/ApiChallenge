using Microsoft.EntityFrameworkCore;
using ContactsApi.Models;

namespace ContactsApi.Data
{
    public class ContactsApiContext : DbContext
    {
        public ContactsApiContext (DbContextOptions<ContactsApiContext> options)
            : base(options)
        {
        }
        public DbSet<Contact>? Contact { get; set; }

    }
}


