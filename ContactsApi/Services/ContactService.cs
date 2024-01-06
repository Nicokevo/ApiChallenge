// En el archivo Services/ContactService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Data;
using ContactsApi.Models;
using ContactsApi.Repositories;

namespace ContactsApi.Services
{
    public class ContactService : IContactService
    {
        private readonly GenericRepository<Contact> _contactRepository;
        private readonly UnitOfWork _unitOfWork;

        public ContactService(GenericRepository<Contact> contactRepository, UnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _contactRepository.GetAll();
        }

        public async Task<Contact> GetContact(int id)
        {
            return await _contactRepository.GetById(id);
        }

        public async Task CreateContact(Contact contact)
        {
            await _contactRepository.Create(contact);
        }

        public async Task UpdateContact(Contact contact)
        {
            await _contactRepository.Update(contact);
        }

        public async Task DeleteContact(int id)
        {
            await _contactRepository.Delete(id);
        }

        public async Task<IEnumerable<Contact>> SearchByEmailOrPhone(string searchTerm)
        {
            return await _contactRepository.SearchByEmailOrPhone(searchTerm);
        }

        public async Task<IEnumerable<Contact>> GetByLocation(string state, string city)
        {
            return await _contactRepository.GetByLocation(state, city);
        }
    }
}
