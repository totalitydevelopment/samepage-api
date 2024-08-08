using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers.Couples;

[ApiController]
[Route("couples/{coupleId}/sections")]
public class CouplesSectionsController : ControllerBase
{
    public CouplesSectionsController()
    {
    }

    [HttpGet()]
    public async Task<ActionResult<dynamic>> Get()
    {
        var response = new
        {
            Sections = new List<dynamic>
            {
                new
                {
                    Id = Guid.NewGuid(),
                    Title = "Finances",
                    Excluded = true,
                    Completed = true,
                    DisplayOrder = 1
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Title = "Parenting",
                    Excluded = true,
                    Completed = false,
                    DisplayOrder = 2
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Title = "Sex Life",
                    Excluded = true,
                    Completed = false,
                    DisplayOrder = 3
                },
            }

        };

        return Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<dynamic>> Post(dynamic sections)
    {
        return NoContent();
    }
}
