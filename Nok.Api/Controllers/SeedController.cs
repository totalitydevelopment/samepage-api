using Microsoft.AspNetCore.Mvc;
using Nok.Core.Aggregates.Register;
using Nok.Infrastructure.Data;

namespace Nok.Api.Controllers;

[ApiController]
[Route("seed")]
public class SeedController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public SeedController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpPost()]
    public ActionResult Post()
    {
        //check if the user below exists and add if not
        var member = _databaseContext.Members.Find(new Guid("5B9FC753-DE88-4B40-B36C-EFC76464FC71"));

        if (member == null)
        {
            member = new Member(new Guid("5B9FC753-DE88-4B40-B36C-EFC76464FC71"), new Name("Mr", "Simon", "Mark", "Gonzalez Lewis"));

            member.SetContactDetails(new ContactDetails("test@mailtrap.com", "0123456789", "0123456789", "0123456789"));
            member.SetAddress(new Address("Address 1", "", "London", "E1 1AA", "UK"));
            member.SetVehicle(new Vehicle("ABC123", "Ford", "Fiesta", "Red", "No notes"));
            member.SetDateOfBirth(new DateOfBirth(1980, 1, 1));
            member.SetNextOfKin(new NextOfKin(
                new Guid("BC79CFDC-DB27-438C-A00F-EF97D01CBFE9"),
                new Name("Mrs", "Eva", "Maria", "Gonzalez Lewis"),
                new ContactDetails("test1@mailtrap.com", "0123456789", "0123456789", "0123456789"),
                "Wife")
                );

            _databaseContext.Members.Add(member);
            _databaseContext.SaveChanges();
        }

        return Ok();
    }

}
