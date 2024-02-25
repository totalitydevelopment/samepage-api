using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nok.Core.Aggregates.Register;
using Nok.Infrastructure.Data;

namespace Nok.Api.Controllers;

[ApiController]
[Route("members")]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;

    public MembersController(ILogger<MembersController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpPost()]
    public ActionResult<Guid> Post([FromBody] CreateMemberRequest newMember)
    {
        var member = new Member(Guid.NewGuid(), new Name(newMember.Title, newMember.FirstName, newMember.MiddleName, newMember.LastName));

        if (!string.IsNullOrEmpty(newMember.Email))
        {
            member.SetContactEmail(newMember.Email);
        }

        _databaseContext.Members.Add(member);
        _databaseContext.SaveChanges();

        return member.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<GetMemberResponse> Get(Guid id)
    {
        var member = _databaseContext.Members
            .Include(x => x.NextOfKins)
            .FirstOrDefault(x=>x.Id==id);

        if (member == null)
        {
            return NotFound();
        }

        return new GetMemberResponse
        {
            Id = member.Id,
            Name = new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
            Contact = member.Contact == null ? null : new ContactResponse(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber),
            Vehicle = member.Vehicle == null ? null : new VehicleResponse(member.Vehicle.RegistrationNumber, member.Vehicle.Make, member.Vehicle.Model, member.Vehicle.Colour, member.Vehicle.Notes),
            HasImage = member.HasImage,
            DateOfBirth = member.DateOfBirth == null ? null : new DateOfBirthResponse(member.DateOfBirth.Year, member.DateOfBirth.Month, member.DateOfBirth.Day),
            NextOfKins = member.NextOfKins.Select(member => new NextOfKinResponse(member.Id, new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname), new ContactResponse(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber), member.Relationship)).ToList(), 
            ImageUrl = member.HasImage ? "https://noktemp.blob.core.windows.net/images/" + member.ImageUrl : string.Empty
        };
    }

    [HttpGet()]
    public ActionResult<IEnumerable<GetMemberListItem>> GetList([FromQuery] string? searchTerm = null)
    {
        IEnumerable<Member> members = _databaseContext.Members;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            members = members.Where(u => u.Name.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || u.Name.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        return members.Select(u => new GetMemberListItem
        {
            Id = u.Id,
            Name = new NameResponse(u.Name.Title, u.Name.FirstName, u.Name.MiddleName, u.Name.Surname),
            DateOfBirth = u.DateOfBirth == null ? null : new DateOfBirthResponse(u.DateOfBirth.Year, u.DateOfBirth.Month, u.DateOfBirth.Day),

            // DateOfBirth = u.DateOfBirth,
            KnownTown = u.Address?.Town,
            HasImage = u.HasImage
        }).ToList();
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}
