using FluentValidation;
using Nok.Core.Models;

namespace Nok.Api.Validators.Members;

public class MembersPostValidator : AbstractValidator<MemberRequest>
{
    public MembersPostValidator()
    {
        //RuleFor(member => member.FirstName).NotNull()
        //        .NotEmpty()
        //        .MaximumLength(100)
        //        .WithName(nameof(MemberRequest.FirstName));

        //RuleFor(member => member.LastName).NotNull()
        //        .NotEmpty()
        //        .MaximumLength(100)
        //        .WithName(nameof(CreateMemberRequest.LastName));

        //RuleFor(member => member.MiddleName).MaximumLength(100)
        //        .WithName(nameof(CreateMemberRequest.MiddleName));

        //RuleFor(member => member.Email).NotNull()
        //        .NotEmpty()
        //        .MaximumLength(300)
        //        .EmailAddress()
        //        .WithName(nameof(CreateMemberRequest.Email));
    }
}
