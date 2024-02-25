namespace Nok.Core.Aggregates.Register;

public class NextOfKin : Person
{
    private NextOfKin()
    {
        // Required by EF
    }

    public NextOfKin(Guid id, Name name, ContactDetails contactDetails, string relationship) : base(id, name)
    {
        Relationship = relationship;
        Contact = contactDetails;
    }

    public string Relationship { get; init; } = string.Empty;
}
