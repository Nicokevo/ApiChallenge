using ContactsApi.Helpers;
using ContactsApi.Models;
using FluentValidation;
using static ContactsApi.Helpers.ErrorHelper;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {

        RuleFor(contact => contact.FirstName)
            .NotEmpty().WithMessage(ValidationMessages.FirstNameNotEmpty)
            .Must(BeValidName).WithMessage(ValidationMessages.FirstNameNonNumeric)
            .Length(1, 30); 

        RuleFor(contact => contact.LastName)
            .NotEmpty().WithMessage(ValidationMessages.LastNameNotEmpty)
            .Must(BeValidName).WithMessage(ValidationMessages.LastNameNonNumeric)
            .Length(1, 30); 

        RuleFor(contact => contact.Company).NotEmpty().WithMessage(ValidationMessages.CompanyNotEmpty);
        RuleFor(contact => contact.ProfileImage).NotEmpty().WithMessage(ValidationMessages.ProfileImageNotEmpty);
        RuleFor(contact => contact.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailNotEmpty)
            .EmailAddress().WithMessage(ValidationMessages.EmailInvalidFormat);
        RuleFor(contact => contact.BirthDate)
            .NotEmpty().WithMessage(ErrorHelper.ValidationMessages.BirthDateNotInFuture)
            .Must(BeAValidDate).WithMessage(ErrorHelper.ValidationMessages.BirthDateNotInFuture);
        RuleFor(contact => contact.WorkPhoneNumber)
            .Matches("^[0-9]{10}$").WithMessage(ValidationMessages.WorkPhoneNumberInvalidFormat);
        RuleFor(contact => contact.PersonalPhoneNumber)
            .Matches("^[0-9]{10}$").WithMessage(ValidationMessages.PersonalPhoneNumberInvalidFormat);
        RuleFor(contact => contact.Address).NotEmpty().WithMessage(ValidationMessages.AddressNotEmpty);
    }

    private static bool BeValidName(string name)
    {

        return name.All(char.IsLetter);
    }

    private static bool BeAValidDate(DateTime? birthDate)
    {
        return birthDate <= DateTime.Now;
    }

    public List<string> ValidateContact(Contact contact)
    {
        var validationResult = Validate(contact);
        return validationResult.Errors.Select(error => error.ErrorMessage).ToList();
    }
}
