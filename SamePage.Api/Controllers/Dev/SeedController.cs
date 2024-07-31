using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamePage.Core.Extensions;
using SamePage.Infrastructure.Services;
using SamePage.Infrastructure.Services.Seeding;

namespace SamePage.Api.Controllers.Dev;

[ApiController]
[Authorize]
[Route("seed")]
public class SeedController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDatabaseSeedingService _databaseSeedingService;
    private readonly IAccessIdentifierService _accessIdentityService;

    public SeedController(
        IMapper mapper,
        IDatabaseSeedingService databaseSeedingService,
        IAccessIdentifierService accessIdentityService)
    {
        _mapper = mapper;
        _databaseSeedingService = databaseSeedingService;
        _accessIdentityService = accessIdentityService;
    }

    [HttpPost()]
    [Authorize(Policy = "write:members")]
    public async Task<ActionResult> Post()
    {
        if (!EnvironmentExtensions.IsLocalOrDev)
        {
            return BadRequest();
        }

        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        await _databaseSeedingService.SeedDatabase(accessIdentityId);

        return Ok();
    }
}
