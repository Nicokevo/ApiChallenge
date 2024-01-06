// En el archivo Services/IContactService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContact(int id);
        Task CreateContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int id);
        Task<IEnumerable<Contact>> SearchByEmailOrPhone(string searchTerm);
        Task<IEnumerable<Contact>> GetByLocation(string state, string city);
    }
}
