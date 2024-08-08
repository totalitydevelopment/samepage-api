using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers.Couples;

[ApiController]
[Route("couples/{coupleId}/persons")]
public class CouplesPartnerController : ControllerBase
{
    public CouplesPartnerController()
    {
    }

    [HttpGet()]
    public async Task<ActionResult<dynamic>> GetList()
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
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Evita",
                    LastName = "Gonzalez Lewis",
                },
            }

        };

        return Ok(response);
    }

    [HttpGet("{personId}")]
    public async Task<ActionResult<dynamic>> Get()
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
    public async Task<ActionResult<dynamic>> Post()
    {
        return Ok(Guid.NewGuid());
    }
}
