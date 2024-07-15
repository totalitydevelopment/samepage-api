namespace Nok.Infrastructure.Data.Models;

public interface INextOfKin : IPerson
{
    string Relationship { get; init; }
}
