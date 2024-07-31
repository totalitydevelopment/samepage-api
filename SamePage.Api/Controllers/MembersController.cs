using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamePage.Core.Models;
using SamePage.Core.Validators;
using SamePage.Infrastructure.Services;

namespace SamePage.Api.Controllers;

[ApiController]
[Authorize]
[Route("public/members")]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly IMembersService _membersService;
    private readonly IAccessIdentifierService _accessIdentityService;

    public MembersController(
        ILogger<MembersController> logger,
        IAccessIdentifierService accessIdentityService,
        IMembersService membersService)
    {
        _logger = logger;
        _membersService = membersService;
        _accessIdentityService = accessIdentityService;
    }

    [HttpPost()]
    [Authorize(Policy = "write:members")]
    [ModelValidator]
    public async Task<ActionResult<Guid>> Post([FromBody] MemberRequest newMember)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        return Ok(await _membersService.CreateMemberAsync(accessIdentityId, newMember));
    }

    [HttpGet("{memberId}")]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<MemberResponse>> Get(Guid memberId)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var member = await _membersService.GetMemberAsync(accessIdentityId, memberId);

        return member is null
             ? NotFound()
             : Ok(member);
    }

    [HttpGet()]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<IEnumerable<MemberResponse>>> GetList([FromQuery] string? searchTerm = null)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        // TODO handle member not found

        return Ok(await _membersService.GetMembersAsync(accessIdentityId, searchTerm));

    }

    [HttpPut("{id}")]
    [Authorize(Policy = "write:members")]
    public ActionResult Put(Guid id, [FromBody] MemberRequest updatedMember)
    {
        return NoContent();
    }
}
