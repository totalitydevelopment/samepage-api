using Nok.Core.Extensions;

namespace Nok.Core.Aggregates.Register;

public abstract class Person : GuidDataEntity
{
    public Name Name { get; protected set; }
    public ContactDetails? Contact { get; protected set; }
    public Address? Address { get; protected set; }

    protected Person()
    {
        // Required by EF
    }

    public Person(Guid id, Name name)
    {
        Id = id;
        Name = name;

        CreatedBy = Id;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = Id;
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
