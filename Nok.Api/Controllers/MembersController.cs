using Microsoft.AspNetCore.Mvc;
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
        var member = _databaseContext.Members.Find(id);

        if (member == null)
        {
            return NotFound();
        }

        return new GetMemberResponse
        {
            Id = member.Id,
            Name = new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
            Contact = member.Contact == null ? null : new ContactResponse(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber),
            Vehicle = null
        };
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}
