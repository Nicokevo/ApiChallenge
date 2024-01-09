using ContactsApi.Models;

namespace ContactsApi.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<IEnumerable<Contact>> SearchByEmailOrPhone(string searchTerm);
        Task<IEnumerable<Contact>> GetByState(string state);
        Task<IEnumerable<Contact>> GetByCity(string city);
        Task<IEnumerable<Contact>> GetByLocation(string state, string city);
        Task<bool> ContactExistsWithSameName(int contactId, string firstName, string lastName);
        Task<bool> ContactExistsWithSamePersonalPhoneNumber(int contactId, string? phone);
    }
}
