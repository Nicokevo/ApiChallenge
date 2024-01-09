using ContactsApi.Dtos;
using ContactsApi.Models;

namespace ContactsApi.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllContacts();
        Task<ContactDto> GetContactById(int id);
        Task<Contact> AddContact(ContactDto contactDto);
        Task UpdateContact(int id, ContactDto contactDto);
        Task DeleteContact(int id);
        Task<IEnumerable<ContactDto>> SearchContactsByEmailOrPhone(string searchTerm);
        Task<IEnumerable<ContactDto>> GetContactsByLocation(string state, string city);
        Task<IEnumerable<ContactDto>> SearchContactsByState(string state);
        Task<IEnumerable<ContactDto>> SearchContactsByCity(string city);
        Task<bool> ContactExistsWithSameName(int contactId, string? firstName, string? lastName);
        Task<bool> ContactExistsWithSamePersonalPhoneNumber(int id, string? phone);
    }
}
