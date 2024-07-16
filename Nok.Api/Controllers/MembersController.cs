using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nok.Api.Extensions;
using Nok.Api.Services;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Data.Models;

namespace Nok.Api.Controllers;

[ApiController]
[Authorize]
[Route("members")]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;
    private readonly IAccessIdentifierService _accessIdentityService;

    public MembersController(ILogger<MembersController> logger, DatabaseContext databaseContext, IAccessIdentifierService accessIdentityService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _accessIdentityService = accessIdentityService;
    }

    [HttpPost()]
    [Authorize(Policy = "write:members")]
    public ActionResult<Guid> Post([FromBody] CreateMemberRequest newMember)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var member = new Member()
        {
            Id = Guid.NewGuid(),
            Name = newMember.Name,
            Address = newMember.Address,
            ContactDetails = newMember.ContactDetails,
            DateOfBirth = newMember.DateOfBirth,
            Vehicle = newMember.Vehicle,
            ImageUrl = newMember.ImageUrl
        };

        accessIdentity.Members.Add(member);
        _databaseContext.SaveChanges();

        return member.Id;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "read:members")]
    public ActionResult<GetMemberResponse> Get(Guid id)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        // TODO Users can get their members.
        // TODO APIs can get any member

        var member = _databaseContext.Members
            .Include(x => x.NextOfKin)
            .FirstOrDefault(x => x.Id == id);

        if (member is null)
        {
            return NotFound();
        }

        return new GetMemberResponse
        {
            Id = member.Id,
            Name = new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
            Contact = member.ContactDetails == null ? null : new ContactResponse(member.ContactDetails.Email, member.ContactDetails.HomeNumber, member.ContactDetails.WorkNumber, member.ContactDetails.MobileNumber),
            Vehicle = member.Vehicle == null ? null : new VehicleResponse(member.Vehicle.RegistrationNumber, member.Vehicle.Make, member.Vehicle.Model, member.Vehicle.Colour, member.Vehicle.Notes),
            HasImage = member.HasImage(),
            DateOfBirth = member.DateOfBirth == null ? null : new DateOfBirthResponse(member.DateOfBirth.Year, member.DateOfBirth.Month, member.DateOfBirth.Day),
            NextOfKin = member.NextOfKin.Select(member => new NextOfKinResponse(member.Id, new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname), new ContactResponse(member.ContactDetails.Email, member.ContactDetails.HomeNumber, member.ContactDetails.WorkNumber, member.ContactDetails.MobileNumber), member.Relationship)).ToList(),
            ImageUrl = member.HasImage() ? "https://noktemp.blob.core.windows.net/images/" + member.ImageUrl : string.Empty
        };
    }

    [HttpGet()]
    [Authorize(Policy = "read:members")]
    public ActionResult<IEnumerable<GetMemberListItem>> GetList([FromQuery] string? searchTerm = null)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        // TODO Users can get their members.
        // TODO APIs can get any member

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
            KnownTown = u.Address?.Town,
            HasImage = u.HasImage()
        }).ToList();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "write:members")]
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}
