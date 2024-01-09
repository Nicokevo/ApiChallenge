using Abp.Domain.Uow;
using AutoMapper;
using ContactsApi.Data;
using ContactsApi.Dtos;
using ContactsApi.Models;
using ContactsApi.Repositories;
using ContactsApi.Services;
using FluentValidation;
using OpenQA.Selenium.Support.UI;
using SendGrid.Helpers.Errors.Model;
using IUnitOfWork = ContactsApi.Data.IUnitOfWork;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ContactService> _logger;
    private readonly IValidator<Contact> _contactValidator;
    private readonly IMapper _mapper;

    public ContactService(IUnitOfWork unitOfWork, ILogger<ContactService> logger, IValidator<Contact> contactValidator, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _contactValidator = contactValidator ?? throw new ArgumentNullException(nameof(contactValidator));
        _mapper = mapper != null ? mapper : throw new ArgumentNullException(nameof(mapper));

    }

    public async Task<bool> ContactExistsWithSameName(int contactId, string? firstName, string? lastName)
    {
        return await _unitOfWork.ContactRepository.ContactExistsWithSameName(contactId, firstName, lastName);
    }

    public async Task<bool> ContactExistsWithSamePersonalPhoneNumber(int contactId, string? phone)
    {
        return await _unitOfWork.ContactRepository.ContactExistsWithSamePersonalPhoneNumber(contactId, phone);
    }
    public async Task<IEnumerable<ContactDto>> GetAllContacts()
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.GetAll();
            return MapContactToDto(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all contacts.");
            throw;
        }
    }

    public async Task<ContactDto?> GetContactById(int id)
    {
        try
        {
            var contact = await _unitOfWork.ContactRepository.GetById(id);

            if (contact == null)
            {
                return null;
            }

            return MapContactToDto(contact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving the contact with ID {id}.");
            throw;
        }
    }

   public async Task<Contact> AddContact(ContactDto contactDto)
    {
        var contact = MapDtoToContact(contactDto);
        ValidateContact(contact);

        try
        {
            await _unitOfWork.ContactRepository.Create(contact);
            await _unitOfWork.SaveAsync();

            return contact;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding the contact.");
            throw;
        }
    } 
    public async Task UpdateContact(int id, ContactDto contactDto)
    {
        try
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid contact ID.", nameof(id));
            }

            var existingContact = await _unitOfWork.ContactRepository.GetById(id);
            if (existingContact == null)
            {
                throw new NotFoundException($"Contact with ID {id} not found.");
            }

         
            _mapper.Map(contactDto, existingContact);

        
            ValidateContact(existingContact);

            await _unitOfWork.ContactRepository.Update(existingContact);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the contact.");
            throw;
        }
    }

    public async Task DeleteContact(int id)
    {
        try
        {
            await _unitOfWork.ContactRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting the contact with ID {id}.");
            throw;
        }
    }

    public async Task<IEnumerable<ContactDto>> SearchContactsByEmailOrPhone(string searchTerm)
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.SearchByEmailOrPhone(searchTerm);
            return contacts.Select(MapContactToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching for contacts by email or phone: {searchTerm}.");
            throw;
        }
    }

    public async Task<IEnumerable<ContactDto>> GetContactsByLocation(string state, string city)
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.GetByLocation(state, city);
            return contacts.Select(MapContactToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving contacts by location: {state}, {city}.");
            throw;
        }
    }

    public async Task<IEnumerable<ContactDto>> SearchContactsByState(string state)
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.GetByState(state);
            return contacts.Select(MapContactToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching for contacts by state: {state}.");
            throw;
        }
    }

    public async Task<IEnumerable<ContactDto>> SearchContactsByCity(string city)
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.GetByCity(city);
            return contacts.Select(MapContactToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching for contacts by city: {city}.");
            throw;
        }
    }

    public async Task<IEnumerable<ContactDto>> SearchContactsByLocation(string state, string city)
    {
        try
        {
            var contacts = await _unitOfWork.ContactRepository.GetByLocation(state, city);
            return contacts.Select(MapContactToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching for contacts by location: {state}, {city}.");
            throw;
        }
    }

    private void ValidateContact(Contact contact)
    {
        var validationResult = _contactValidator.Validate(contact);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            _logger.LogError($"Validation error: {errorMessages}");
            throw new ValidationException($"Validation error: {errorMessages}");
        }
    }

    private IEnumerable<ContactDto> MapContactToDto(IEnumerable<Contact> contacts)
    {
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    private ContactDto MapContactToDto(Contact contact)
    {
        return _mapper.Map<ContactDto>(contact);
    }

    private Contact MapDtoToContact(ContactDto contactDto)
    {
        return _mapper.Map<Contact>(contactDto);
    }

   
 

}
