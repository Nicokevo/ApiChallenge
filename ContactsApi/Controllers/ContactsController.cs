using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using ContactsApi.Helpers;
using ContactsApi.Models;
using ContactsApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

[Route("api/contacts")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    private readonly ILogger<ContactController> _logger;

    public ContactController(
        IContactService contactService,

        ILogger<ContactController> logger)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
   
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        try
        {
            var contacts = await _contactService.GetAllContacts();

            if (contacts == null || !contacts.Any())
            {
        
                return NotFound("No contacts found.");
            }

            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorHelper.ContactServiceErrors.GetAllContactsError);
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        try
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
            {
                return Problem($"No contact found with ID: {id}", statusCode: 404);
            }

            return Ok(contact);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ErrorHelper.ContactServiceErrors.GetContactByIdError, id);
            return Problem($"No contact found with ID: {id}", statusCode: 404);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting contact with ID: {id}");
            return Problem($"Internal server error getting contact with ID: {id}", statusCode: 500);
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactDto contactDto)
    {
        try
        {
            var existingContact = await _contactService.GetContactById(contactDto.Id);
            var Id = contactDto.Id;
           
            var phone = contactDto.PersonalPhoneNumber;


            if (existingContact != null)
            {
                return Conflict(new { Message = $"Contact with ID {Id} already exists." });
            }
            if(await _contactService.ContactExistsWithSamePersonalPhoneNumber(Id, phone))
            {
                return BadRequest(ErrorHelper.ContactServiceErrors.ContactExistsWithSamePhone);
            }
            if (await _contactService.ContactExistsWithSameName(Id, contactDto.FirstName, contactDto.LastName))
            {
                return BadRequest(ErrorHelper.ContactServiceErrors.ContactExistsWithSameName);
            }
  
            await _contactService.AddContact(contactDto);

            return Ok(new { Message = $"Contact add successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorHelper.ContactServiceErrors.AddContactError);
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactDto contactDto)
    {
        try
        {
            
            if (await _contactService.ContactExistsWithSameName(id, contactDto.FirstName, contactDto.LastName))
            {
                return BadRequest(ErrorHelper.ContactServiceErrors.ContactExistsWithSameName);
            }

            await _contactService.UpdateContact(id, contactDto);

            return Ok(new { Message = $"Contact with ID {id} updated successfully." });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Errors.Select(error => error.ErrorMessage).ToList() });
        }
        catch (NotFoundException)
        {
            return NotFound(string.Format(ErrorHelper.ContactServiceErrors.ContactNotFound, id));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Errors = new List<string> { ex.Message } });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, string.Format(ErrorHelper.ContactServiceErrors.UpdateContactError, id));
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try
        {
            var existingContact = await _contactService.GetContactById(id);

            if (existingContact == null)
            {
                return NotFound(string.Format(ErrorHelper.ContactServiceErrors.DeleteContactError, id));
            }

            await _contactService.DeleteContact(id);

            return Ok(new { Message = $"Contact with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, string.Format(ErrorHelper.ContactServiceErrors.DeleteContactError, id));
            return StatusCode(500, "Internal server error");
        }

    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchContacts([FromQuery] string searchTerm)
    {
        try
        {
            var contacts = await _contactService.SearchContactsByEmailOrPhone(searchTerm);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorHelper.ContactServiceErrors.SearchContactsError);
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }
    }

    [HttpGet("state")]
    public async Task<IActionResult> SearchContactsByState([FromQuery] string state)
    {
        try
        {
            string estadoNormalizado = NormalizeAndLowercase(state);
            var contacts = await _contactService.SearchContactsByState(estadoNormalizado);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching contacts by state: {state}");
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }
    }
    private string NormalizeAndLowercase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

    
        string normalized = input.ToLowerInvariant();


        normalized = normalized.Trim();


        while (normalized.Contains("  "))
        {
            normalized = normalized.Replace("  ", " ");
        }

        return normalized;
    }




    [HttpGet("city")]
    public async Task<IActionResult> SearchContactsByCity([FromQuery] string city)
    {
        try
        {
            var contacts = await _contactService.SearchContactsByCity(city);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching contacts by city: {city}");
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);
        }

    }

    [HttpGet("location")]
    public async Task<IActionResult> SearchContactsByLocation([FromQuery] string state, [FromQuery] string city)
    {
        try
        {
            var contacts = await _contactService.GetContactsByLocation(state, city);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching contacts by location: {state}, {city}");
            return StatusCode(500, ErrorHelper.ServiceErrors.InternalServerError);

        }
    }


}
