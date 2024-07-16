using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nok.Core.Extensions;
using Nok.Infrastructure.Data;

namespace Nok.Api.Controllers.Test;

[ApiController]
[Authorize]
[Route("seed")]
public class SeedController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public SeedController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpPost()]
    public ActionResult Post()
    {
        if (!EnvironmentExtensions.IsLocalOrDev)
        {
            return BadRequest();
        }

        var seedDataGenerator = new SeedDataGenerator();

        _databaseContext.Members.AddRange(seedDataGenerator.Members);
        _databaseContext.SaveChanges();

        return Ok();
    }
}
