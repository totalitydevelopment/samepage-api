using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nok.Core.Aggregates.Register;
using Nok.Infrastructure.Data;

namespace Nok.Api.Controllers;

[ApiController]
[Authorize]
[Route("members/{memberId}/next-of-kins")]
public class NextOfKinsController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;

    public NextOfKinsController(ILogger<MembersController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpPost()]
    public ActionResult<Guid> Post([FromRoute] Guid memberId, [FromBody] CreateNextOfKinRequest newNok)
    {
        var member = _databaseContext.Members.Include(x => x.NextOfKins)
            .FirstOrDefault(x => x.Id == memberId);

        if (member == null)
        {
            return NotFound();
        }

        var nextOfKin = new NextOfKin(
            Guid.NewGuid(),
            new Name(newNok.Title, newNok.FirstName, newNok.MiddleName, newNok.LastName),
            new ContactDetails(newNok.Email, string.Empty, string.Empty, string.Empty),
            newNok.Relationship);

        member.SetNextOfKin(nextOfKin);

        _databaseContext.Members.Update(member);
        _databaseContext.SaveChanges();

        return nextOfKin.Id;
    }

    [HttpGet("{nokId}")]
    public ActionResult<GetNokResponse> Get([FromRoute] Guid memberId, [FromRoute] Guid nokId)
    {
        var member = _databaseContext.Members.Include(x => x.NextOfKins)
            .FirstOrDefault(x => x.Id == memberId);

        if (member == null)
        {
            return NotFound();
        }

        var nok = member.NextOfKins.FirstOrDefault(x => x.Id == nokId);

        if (nok == null)
        {
            return NotFound();
        }

        return new GetNokResponse
        {
            Id = nok.Id,
            Name = new NameResponse(nok.Name.Title, nok.Name.FirstName, nok.Name.MiddleName, nok.Name.Surname),
            Relationship = nok.Relationship,
        };
    }

    [HttpGet()]
    public ActionResult<List<GetNokResponse>> GetAll([FromRoute] Guid memberId)
    {
        var member = _databaseContext.Members.Include(x => x.NextOfKins)
            .FirstOrDefault(x => x.Id == memberId);

        if (member == null)
        {
            return NotFound();
        }

        return member.NextOfKins.Select(x => new GetNokResponse
        {
            Id = x.Id,
            Name = new NameResponse(x.Name.Title, x.Name.FirstName, x.Name.MiddleName, x.Name.Surname),
            Relationship = x.Relationship,
        }).ToList();
    }
}
