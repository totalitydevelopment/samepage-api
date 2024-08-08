using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers.Couples;

[ApiController]
[Route("couples")]
public class CouplesController : ControllerBase
{
    public CouplesController()
    {
    }

    
    [HttpGet("{coupleId}")]
    public async Task<ActionResult<dynamic>> Get(Guid coupleId)
    {
        // I need a respone object that will contain the following:
        var response = new
        {
            Persons = new List<dynamic>
            {
                new
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Simon",
                    LastName = "Gonzalez Lewis",
                }
            }

        };

        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult<dynamic>> Post(dynamic couple)
    {
        return Ok(Guid.NewGuid());
    }
}
