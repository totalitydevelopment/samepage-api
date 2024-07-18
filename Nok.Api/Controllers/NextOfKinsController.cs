using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Api.Extensions;
using Nok.Core.Validators;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Services;

namespace Nok.Api.Controllers;

[ApiController]
[Authorize]
[Route("members/{memberId}/next-of-kins")]
public class NextOfKinsController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;
    private readonly IAccessIdentifierService _accessIdentityService;
    private readonly INextOfKinService _nextOfKinService;

    public NextOfKinsController(
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

        // TODO handle member not found

        return Ok(await _nextOfKinService.CreateNextOfKinAsync(accessIdentityId, memberId, newNextOfKin));
    }

    [HttpGet("{nextOfKinId}")]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<Core.Models.NextOfKinResponse>> Get([FromRoute] Guid memberId, [FromRoute] Guid nextOfKinId)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        // TODO handle member not found
        // TODO handle nextOfKin not found

        return Ok(await _nextOfKinService.GetNextOfKinAsync(accessIdentityId, memberId, nextOfKinId));
    }

    [HttpGet()]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<IEnumerable<Core.Models.NextOfKinResponse>>> GetAll([FromRoute] Guid memberId)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        // TODO handle member not found

        return Ok(await _nextOfKinService.GetNextOfKinAsync(accessIdentityId, memberId));
    }
}
