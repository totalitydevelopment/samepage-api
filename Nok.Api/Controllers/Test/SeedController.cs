using Microsoft.AspNetCore.Mvc;
using Nok.Core.Extensions;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Test.Database.Seeder;

namespace Nok.Api.Controllers.Test;

[ApiController]
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
