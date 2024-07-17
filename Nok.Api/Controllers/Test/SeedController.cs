using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Api.Extensions;
using Nok.Core.Extensions;
using Nok.Infrastructure.Services;

namespace Nok.Api.Controllers.Test;

[ApiController]
[Authorize]
[Route("seed")]
public class SeedController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMembersService _membersService;
    private readonly IAccessIdentifierService _accessIdentityService;

    public SeedController(
        IMapper mapper,
        IMembersService membersService,
        IAccessIdentifierService accessIdentityService)
    {
        _mapper = mapper;
        _membersService = membersService;
        _accessIdentityService = accessIdentityService;
    }

    [HttpPost()]
    public async Task<ActionResult> Post()
    {
        if (!EnvironmentExtensions.IsLocalOrDev)
        {
            return BadRequest();
        }

        var accessIdentityId = await _accessIdentityService.GetOrCreateByClaimsAsync(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var seedDataGenerator = new SeedDataGenerator();

        foreach (var member in seedDataGenerator.Members)
        {
            await _membersService.CreateMemberAsync(accessIdentityId, member);
        }

        return Ok();
    }
}
