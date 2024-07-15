using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Data.Models;
using System.Security.Claims;
using System.Security.Principal;

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
    public ActionResult<Guid> Post([FromBody] CreateMemberRequest newMember)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var member = new Member()
        {
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
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}


public interface IAccessIdentifierService
{
    AccessIdentifier GetOrAddByClaims(IEnumerable<Claim> claims);
}

internal class AccessIdentifierService : IAccessIdentifierService
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;

    public AccessIdentifierService(ILogger<MembersController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public AccessIdentifier GetOrAddByClaims(IEnumerable<Claim> claims)
    {
        var azureOid = claims.GetAzureOid();

        // Get or create new AccessIdentifier
        var identifier = _databaseContext.AccessIdentifiers.FirstOrDefault(x => x.AzureOid == azureOid)
            ?? new AccessIdentifier()
            {
                AzureOid = azureOid,
                Id = Guid.NewGuid(),
                Type = claims.GetAccessIdentifierType()
            };

        _databaseContext.Add(identifier);
        _databaseContext.SaveChanges();

        return identifier;
    }
}


internal static class ClaimExtensions
{
    private const string OidValueTypeString = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    private const string ScopeValueTypeString = "http://schemas.microsoft.com/identity/claims/scope";

    public static IEnumerable<Claim> GetClaims(this IIdentity securityPrincipleIdentity)
        => ((ClaimsIdentity)securityPrincipleIdentity).Claims;

    public static Guid GetAzureOid(this IEnumerable<Claim> claims)
        => Guid.Parse(claims.Single(x => x.Type is OidValueTypeString).Value);

    public static AccessIdentifierType GetAccessIdentifierType(this IEnumerable<Claim> claims)
        => claims.First(x => x.Type is ScopeValueTypeString).Value.StartsWith("app.", StringComparison.InvariantCultureIgnoreCase)
            ? AccessIdentifierType.Api
            : AccessIdentifierType.User;
}
