using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Uplansi.Core.Utils;

namespace Uplansi.Core.DTOs;

public record AccountRegisterModel : IValidatableObject
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? DisplayName { get; init; }
    public string? Country { get; init; }
    public string? Language { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResult = new List<ValidationResult>();
        validationResult.AddRange(ValidateField(DisplayName, nameof(DisplayName), TextHelper.RegExp.PersonNameRegExp, false));
        validationResult.AddRange(ValidateField(UserName, nameof(UserName), TextHelper.RegExp.UserNameRegExp));
        validationResult.AddRange(ValidateField(Password, nameof(Password), null));

        return validationResult;
    }

    private static IEnumerable<ValidationResult> ValidateField(string? value, string fieldName,
        string? regExp, bool isRequired = true)
    {
        if (!isRequired) yield break;
        
        if (IsNullOrEmpty(value, fieldName, out var result))
        {
            yield return result;
        }
        else if (!string.IsNullOrEmpty(regExp) && !Regex.IsMatch(value, regExp))
        {
            yield return new ValidationResult(
                $"The {fieldName} field is invalid.", new[] { fieldName });
        }
    }

    private static bool IsNullOrEmpty(string? value, string fieldName, out ValidationResult result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = new ValidationResult(
                $"The {fieldName} field is required.", new[] { fieldName });
            return true;
        }

        result = default!;
        return false;
    }
}