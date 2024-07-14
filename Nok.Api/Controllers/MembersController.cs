using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Api.Validators;
using Nok.Core.Interfaces;
using Nok.Core.Models;

namespace Nok.Api.Controllers;

[ApiController]
//[Authorize]
[Route("public/members")]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly IMembersService _membersService;
    private readonly IMapper _mapper;

    public MembersController(ILogger<MembersController> logger, IMembersService membersService, IMapper mapper)
    {
        _logger = logger;
        _membersService = membersService;
        _mapper = mapper;
    }

    [HttpPost()]
    [ModelValidator]
    public async Task<ActionResult<Guid>> Post([FromBody] CreateMemberRequest newMember)
    {
        var memberId = await _membersService.CreateMemberAsync(new CreateMember(newMember.Title, newMember.FirstName, newMember.MiddleName, newMember.LastName, newMember.Email));

        return memberId;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMemberResponse>> Get(Guid id)
    {
        var member = await _membersService.GetMemberAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GetMemberResponse>(member));
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<GetMemberListItemResponse>>> GetListAsync([FromQuery] string? searchTerm = null)
    {
        var members = await _membersService.GetListAsync(searchTerm);

        if (members == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<GetMemberListItemResponse>>(members));
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] CreateMemberRequest updatedMember)
    {
        return NoContent();
    }
}
