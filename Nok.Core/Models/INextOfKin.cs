
namespace Nok.Core.Models;

public interface INextOfKin
{
    AddressDto Address { get; init; }
    ContactDetailsDto Contact { get; init; }
    NameDto Name { get; init; }
    string Relationship { get; init; }
}
