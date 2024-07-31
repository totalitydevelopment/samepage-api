using SamePage.Core.Validators;

namespace SamePage.Core.Models;

public record NameDto(string? Title, string FirstName, string? MiddleName, string Surname) : BaseValidationModelRecord<NameDto>;
