using Microsoft.AspNetCore.Mvc;

namespace SamePage.Api.Controllers;

[ApiController]
[Route("questions")]
public class QuestionsController : ControllerBase
{
    public QuestionsController()
    {
    }

    [HttpGet()]
    public async Task<ActionResult<dynamic>> GetList()
    {
        // I need a respone object that will contain the following:
        // - A list of questions
        // - A list of answers
        // - A list of comments

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
}
