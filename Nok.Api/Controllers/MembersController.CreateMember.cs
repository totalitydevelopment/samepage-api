using Ardalis.SharedKernel;

namespace Nok.Api.Controllers;

//public class Address
//{
//    required public string Address1 { get; init; }
//    public string? Address2 { get; init; }
//    required public string Town { get; init; }
//    required public string Postcode { get; init; }
//    public string? Country { get; init; }
//}

//public class User : Person
//{
//    public bool HasImage { get; init; }
//    public string? ImageUrl { get; init; }
//    required public NextOfKin NextOfKin { get; init; }
//    public string? NationalInsuranceNumber { get; init; }
//    public Vehicle? Vehicle { get; init; }
//}

//public class Vehicle
//{
//    required public string Registration { get; init; }
//    public string? Make { get; init; }
//    public string? Model { get; init; }
//    public string? Colour { get; init; }
//    public string? Notes { get; init; }
//}

//public class NextOfKin : Person
//{
//    required public string Relationship { get; init; }
//}
public class CreateMemberRequest
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

public class GetMemberResponse: BaseIdResponse
{
    required public NameResponse Name { get; init; }
    required public ContactResponse? Contact { get; init; }
    required public VehicleResponse? Vehicle { get; init; }
}

public class GetMemberListItem
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

