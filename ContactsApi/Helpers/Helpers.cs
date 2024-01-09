using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Helpers
{
    public static class ErrorHelper
    {
        public static class ValidationMessages
        {
            public const string FirstNameNotEmpty = "The contact's first name cannot be empty.";
            public const string FirstNameNonNumeric = "The contact's first name cannot be numeric.";
            public static string LastNameNonNumeric = "The contact's last name cannot be numeric.";
            public const string LastNameNotEmpty = "The contact's last name cannot be empty.";
            public const string CompanyNotEmpty = "The contact's company cannot be empty.";
            public const string ProfileImageNotEmpty = "The contact's profile image cannot be empty.";
            public const string EmailNotEmpty = "The contact's email cannot be empty.";
            public const string EmailInvalidFormat = "The email format is invalid.";
            public const string BirthDateNotInFuture = "The birth date cannot be in the future.";
            public const string WorkPhoneNumberInvalidFormat = "The work phone number must be 10 digits.";
            public const string PersonalPhoneNumberInvalidFormat = "The personal phone number must be 10 digits.";
            public const string AddressNotEmpty = "The contact's address cannot be empty.";
            public const string IdGreaterThanZero = "The contact's ID must be greater than 0.";
            public const string IdMismatch = "Id mismatch between the route and the request body.";

        }

        public static class ContactServiceErrors
        {
            public const string GetAllContactsError = "Error retrieving all contacts.";
            public const string GetContactByIdError = "Error retrieving the contact with ID {0}.";
            public const string AddContactError = "Error adding the contact.";
            public const string DeleteContactError = "Error deleting the contact. The contact with ID {0} does not exist.";
            public const string UpdateContactError = "Error updating the contact.";
            public const string SearchContactsError = "Error searching for contacts.";
            public const string GetContactsByLocationError = "Error searching for contacts by location.";
            public const string ContactNotFound = "Contact with ID {0} not found.";
            public const string ContactExistsWithSameName = "A contact with the same name already exists.";
            public const string ContactExistsWithSamePhone= "A contact with the same personal phone number already exists.";
        }
    

        public static class ServiceErrors
        {
            public const string InternalServerError = "Internal server error.";
        }

     
    }


}