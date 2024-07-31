namespace SamePage.Core.Aggregates.Register;

public class ContactDetails
{
    private ContactDetails() { }

    public ContactDetails(string email, ContactDetails? contactDetails = null)
    {
        Email = email;

        if (contactDetails is not null)
        {
            HomeNumber = contactDetails.HomeNumber;
            WorkNumber = contactDetails.WorkNumber;
            MobileNumber = contactDetails.MobileNumber;
        }
    }

    public string Email { get; init; } = string.Empty;
    public string HomeNumber { get; init; } = string.Empty;
    public string WorkNumber { get; init; } = string.Empty;
    public string MobileNumber { get; init; } = string.Empty;
}
