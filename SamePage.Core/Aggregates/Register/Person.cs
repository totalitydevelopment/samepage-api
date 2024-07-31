using SamePage.Core.Extensions;

namespace SamePage.Core.Aggregates.Register;

public abstract class Person : GuidDataEntity
{
    public Name Name { get; protected set; }
    public ContactDetails? Contact { get; protected set; }
    public Address? Address { get; protected set; }

    protected Person()
    {
        // Required by EF
    }

    public Person(Guid id, Name name, Guid accessIdentifiedId)
    {
        Id = id;
        Name = name;

        CreatedBy = accessIdentifiedId;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = accessIdentifiedId;
        UpdatedDate = SystemTime.UtcNow();
    }

    public void SetContactDetails(ContactDetails contactDetails)
    {
        Contact = contactDetails;
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }
}
