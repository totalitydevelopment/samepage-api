using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers.Couples;

[ApiController]
[Route("couples/{coupleId}/sections/{sectionId}/questions")]
public class CouplesQuestionsController : ControllerBase
{
    public CouplesQuestionsController()
    {
    }

    [HttpGet()]
    public async Task<ActionResult<dynamic>> GetList()
    {
        var response = new
        {
            Questions = new List<dynamic>
            {
                new
                {
                    Id = Guid.NewGuid(),
                    Title = "What is the capital of Nigeria?",
                    Hint = "I need to know the capital of Nigeria",
                    Answers = new List<dynamic>
                    {
                        new
                        {
                            Id = Guid.NewGuid(),
                            Body = "The capital of Nigeria is Abuja",
                            Comments = new List<dynamic>
                            {
                                new
                                {
                                    Id = Guid.NewGuid(),
                                    Body = "This is a great answer",
                                }
                            }
                        }
                    }
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Title = "What is the capital of Ghana?",
                    Hint = "I need to know the capital of Ghana",
                    Answers = new List<dynamic>
                    {
                        new
                        {
                            Id = Guid.NewGuid(),
                            Body = "The capital of Ghana is Accra",
                            Comments = new List<dynamic>
                            {
                                new
                                {
                                    Id = Guid.NewGuid(),
                                    Body = "This is a great answer",
                                }
                            }
                        }
                    }
                }
            }

        };

        return Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<dynamic>> Post(dynamic answers)
    {
        return NoContent();
    }
}
