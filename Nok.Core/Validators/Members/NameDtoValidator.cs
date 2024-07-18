using FluentValidation;
using Nok.Core.Models;

namespace Nok.Core.Validators.Members;

public class NameDtoValidator : AbstractValidator<NameDto>
{
    public NameDtoValidator()
    {
        RuleFor(member => member.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(nameof(NameDto.FirstName));

        RuleFor(member => member.Surname)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(nameof(NameDto.Surname));

        RuleFor(member => member.MiddleName)
            .MaximumLength(100)
            .WithName(nameof(NameDto.MiddleName));

        RuleFor(member => member.Title)
            .MaximumLength(100)
            .WithName(nameof(NameDto.Title));
    }
}
