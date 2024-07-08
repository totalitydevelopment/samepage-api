using Microsoft.AspNetCore.Mvc;
using Nok.Core.Aggregates.Register;
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
        if(!EnvironmentExtensions.IsLocalOrDev)
        {
            return BadRequest();
        }

        //check if the user below exists and add if not
        var member = _databaseContext.Members.Find(new Guid("5B9FC753-DE88-4B40-B36C-EFC76464FC71"));

        if (member == null)
        {
            var seedDataGenerator = new SeedDataGenerator();

            _databaseContext.Members.AddRange(seedDataGenerator.Members);
            _databaseContext.SaveChanges();
        }

        return Ok();
    }

}
