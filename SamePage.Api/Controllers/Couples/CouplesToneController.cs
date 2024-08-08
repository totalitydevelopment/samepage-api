using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers.Couples;

[ApiController]
[Route("couples/{coupleId}/tone")]
public class CouplesToneController : ControllerBase
{
    public CouplesToneController()
    {
    }


    [HttpGet]
    public async Task<ActionResult<dynamic>> Get(Guid coupleId)
    {
        var response = new
        {
            Tone = "businessLike"
        };

        return Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<dynamic>> Post(dynamic tone)
    {
        return NoContent();
    }
}
