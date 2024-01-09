using ContactsApi.Data;
using ContactsApi.Models;
using ContactsApi.Repositories;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ContactRepository : IContactRepository 
{
    private readonly ContactsApiContext _context;

    public ContactRepository(ContactsApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAll()
    {
        return await _context.Contact.ToListAsync();
    }


    public async Task<Contact> GetById(int id)
    {
        Console.WriteLine($"Búsqueda del contacto con ID: {id}");

        return await _context.Contact.FirstOrDefaultAsync(contact => contact.Id == id);
    }



    public async Task Create(Contact entity)
    {
        _context.Contact.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Contact entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }


    public async Task Delete(int id)
    {
        var entityToDelete = await _context.Contact.FindAsync(id);
        if (entityToDelete != null)
        {
            _context.Contact.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Contact>> SearchByEmailOrPhone(string searchTerm)
    {
        return await _context.Contact
            .Where(contact => contact.Email.Contains(searchTerm) || contact.WorkPhoneNumber.Contains(searchTerm) || contact.PersonalPhoneNumber.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetByState(string state)
    {
        return await _context.Contact
            .Where(contact => contact.Address.Contains(state))
            .ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetByCity(string city)
    {
        return await _context.Contact
            .Where(contact => contact.Address.Contains(city))
            .ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetByLocation(string state, string city)
    {
        return await _context.Contact
            .Where(contact => contact.Address.Contains(state) && contact.Address.Contains(city))
            .ToListAsync();
    }

    public async Task<bool> ContactExistsWithSameName(int contactId, string firstName, string lastName)
    {
 
        return await _context.Contact
            .Where(c => c.Id != contactId)
            .AnyAsync(c => c.FirstName == firstName && c.LastName == lastName);
    }

   

    public async Task<bool> ContactExistsWithSamePersonalPhoneNumber(int contactId, string? phone)
    {
        return await _context.Contact
            .Where(c => c.Id != contactId)
            .AnyAsync(c => c.PersonalPhoneNumber == phone);
    }

  
}

