using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Infrastructure.Services;

namespace Nok.Api.Controllers;

[ApiController]
[Authorize]
[Route("public")]
public class SearchController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly IMembersService _membersService;
    private readonly IAccessIdentifierService _accessIdentityService;

    public SearchController(
        ILogger<MembersController> logger,
        IAccessIdentifierService accessIdentityService,
        IMembersService membersService)
    {
        _logger = logger;
        _membersService = membersService;
        _accessIdentityService = accessIdentityService;
    }

    [HttpGet()]
    [Authorize(Policy = "read:members:all")]
    [Route("members/search")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetList([FromQuery] string? searchTerm = null)
    {
        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());


        return Ok(await _membersService.GetAllMembersAsync(accessIdentityId, searchTerm));
    }
}
