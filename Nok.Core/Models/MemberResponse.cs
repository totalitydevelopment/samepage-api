namespace Nok.Core.Models;

public class MemberResponse : IMember
{
    public required Guid Id { get; init; }
    public IList<NextOfKinResponse> NextOfKin { get; init; } = [];

    public required NameDto Name { get; init; }
    public required ContactDetailsDto Contact { get; init; }
    public required VehicleDto Vehicle { get; init; }
    public required DateOfBirthDto DateOfBirth { get; init; }
    public required AddressDto Address { get; init; }
    public required string? ImageUrl { get; init; }
}
