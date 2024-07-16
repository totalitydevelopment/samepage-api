using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Api.Validators;
using Nok.Api.Extensions;
using Nok.Api.Services;
using Nok.Core.Interfaces;
using Nok.Core.Models;
using Nok.Infrastructure.Data.Models;

namespace Nok.Api.Controllers;

[ApiController]
//[Authorize]
[Route("public/members")]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly IMembersService _membersService;
    private readonly IAccessIdentifierService _accessIdentityService;
    private readonly IMapper _mapper;

    public MembersController(ILogger<MembersController> logger, IAccessIdentifierService accessIdentityService, IMembersService membersService, IMapper mapper)
    {
        _logger = logger;
        _membersService = membersService;
        _accessIdentityService = accessIdentityService;
        _mapper = mapper;
    }

    [HttpPost()]
    [Authorize(Policy = "write:members")]
    [ModelValidator]
    public async Task<ActionResult<Guid>> Post([FromBody] CreateMemberRequest newMember)
    {
        var memberId = await _membersService.CreateMemberAsync(new CreateMember(newMember.Title, newMember.FirstName, newMember.MiddleName, newMember.LastName, newMember.Email));
        
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

        return memberId;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<GetMemberResponse>> Get(Guid id)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());
            
        var member = await _membersService.GetMemberAsync(id);


        // TODO Users can get their members.
        // TODO APIs can get any member


        if (member is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GetMemberResponse>(member));
    }

    [HttpGet()]
    [Authorize(Policy = "read:members")]
    public async Task<ActionResult<IEnumerable<GetMemberListItemResponse>>> GetListAsync([FromQuery] string? searchTerm = null)
    {
        var accessIdentity = _accessIdentityService.GetOrAddByClaims(HttpContext.User.Identity?.GetClaims()
            ?? throw new UnauthorizedAccessException());

        var members = await _membersService.GetListAsync(searchTerm);

        // TODO Users can get their members.
        // TODO APIs can get any member


        if (members == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<GetMemberListItemResponse>>(members));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "write:members")]
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}
