using Nok.Api.Validators;

namespace Nok.Api.Controllers;

public class CreateMemberRequest : BaseValidationModel<CreateMemberRequest>
{
    public string? Title { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public class BaseIdResponse
{
    required public Guid Id { get; init; }
}

public class GetMemberResponse : BaseIdResponse
{
    required public string? ImageUrl { get; init; }
    required public NameResponse Name { get; init; }
    required public ContactResponse? Contact { get; init; }
    required public VehicleResponse? Vehicle { get; init; }
    required public DateOfBirthResponse? DateOfBirth { get; init; }
    required public bool HasImage { get; init; }
    public List<NextOfKinResponse> NextOfKins { get; internal set; }
}

public class GetNokResponse : BaseIdResponse
{
    required public string? Relationship { get; init; }
    required public NameResponse Name { get; init; }
}

public class GetMemberListItemResponse
{
    required public Guid Id { get; init; }
    required public NameResponse Name { get; init; }
    required public DateOfBirthResponse? DateOfBirth { get; init; }
    required public string? KnownTown { get; init; }
    required public bool HasImage { get; init; }
}

public record NameResponse(string? Title, string FirstName, string? MiddleName, string LastName);
public record ContactResponse(string Email, string HomeNumber, string WorkNumber, string MobileNumber);
public record VehicleResponse(string RegistrationNumber, string Make, string? Model, string? Colour, string? Notes);
public record DateOfBirthResponse(int Year, int? Month, int? Day);

public record NextOfKinResponse(Guid Id, NameResponse Name, ContactResponse Contact, string Relationship);

