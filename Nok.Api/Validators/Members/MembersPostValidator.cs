using FluentValidation;
using Nok.Api.Controllers;

namespace Nok.Api.Validators.Members;

public class MembersPostValidator : AbstractValidator<CreateMemberRequest>
{
    public MembersPostValidator()
    {
        RuleFor(member => member.FirstName).NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithName(nameof(CreateMemberRequest.FirstName));

        RuleFor(member => member.LastName).NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithName(nameof(CreateMemberRequest.LastName));

        RuleFor(member => member.MiddleName).MaximumLength(100)
                .WithName(nameof(CreateMemberRequest.MiddleName));

        RuleFor(member => member.Email).NotNull()
                .NotEmpty()
                .MaximumLength(300)
                .EmailAddress()
                .WithName(nameof(CreateMemberRequest.Email));
    }
}
