using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nok.Api.Controllers
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    [ApiController]
    [Route("users")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UsersController : ControllerBase
    {
        // Dummy list of users (replace this with your actual data source)
        private static readonly List<User> Users = new List<User>
        {
            new User { UserId = "1", FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 5, 15) },
            new User { UserId = "2", FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1985, 8, 22) },
            // Add more users here...
        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

 

        [HttpGet("")]
        public IEnumerable<User> GetUsers()
        {
            // Return the list of users
            return Users;
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(string userId)
        {
            // Find the user by userId
            var user = Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return user;
        }
    }
}
