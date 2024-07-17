namespace Nok.Core.Models;

public class MemberDto
{
    public required Guid Id { get; init; }
    public required NameDto Name { get; init; }
    public required ContactDetailsDto Contact { get; init; }
    public required VehicleDto Vehicle { get; init; }
    public required DateOfBirthDto DateOfBirth { get; init; }
    public required AddressDto Address { get; init; }
    public required string? ImageUrl { get; init; }
    public IList<NextOfKinDto> NextOfKin { get; init; } = [];
}
