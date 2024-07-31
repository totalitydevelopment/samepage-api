using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamePage.Core.Validators;
using SamePage.Infrastructure.Data;
using SamePage.Infrastructure.Services;

namespace SamePage.Api.Controllers;

[ApiController]
[Authorize]
[Route("members/{memberId}/next-of-kin")]
public class NextOfKinController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;
    private readonly IAccessIdentifierService _accessIdentityService;
    private readonly INextOfKinService _nextOfKinService;

    public NextOfKinController(
        ILogger<MembersController> logger,
        DatabaseContext databaseContext,
        IAccessIdentifierService accessIdentityService,
        INextOfKinService nextOfKinService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _accessIdentityService = accessIdentityService;
        _nextOfKinService = nextOfKinService;
    }

    [HttpPost()]
    [Authorize(Policy = "write:members")]
    [ModelValidator]
    public async Task<ActionResult<Guid>> Post([FromRoute] Guid memberId, [FromBody] Core.Models.NextOfKinRequest newNextOfKin)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var nextOfKin = await _nextOfKinService.CreateNextOfKinAsync(accessIdentityId, memberId, newNextOfKin);

        return nextOfKin is null
             ? NotFound()
             : Ok(nextOfKin);
    }

    [HttpGet("{nextOfKinId}")]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<Core.Models.NextOfKinResponse>> Get([FromRoute] Guid memberId, [FromRoute] Guid nextOfKinId)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var nextOfKin = await _nextOfKinService.GetNextOfKinAsync(accessIdentityId, memberId, nextOfKinId);

        return nextOfKin is null
             ? NotFound()
             : Ok(nextOfKin);
    }

    [HttpGet()]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<IEnumerable<Core.Models.NextOfKinResponse>>> GetAll([FromRoute] Guid memberId)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var nextOfKin = await _nextOfKinService.GetNextOfKinAsync(accessIdentityId, memberId);

        return nextOfKin is null
             ? NotFound()
             : Ok(nextOfKin);
    }
}
