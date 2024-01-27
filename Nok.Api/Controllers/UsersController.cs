using Microsoft.AspNetCore.Mvc;

namespace Nok.Api.Controllers
{
    public abstract class Person
    {
        required public Guid Id { get; init; }
        public string? Title { get; init; }
        required public string FirstName { get; init; }
        required public string LastName { get; init; }
        public DateOnly DateOfBirth { get; init; }
        public Address? Address { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Mobile { get; init; }

    }

    public class Address
    {
        required public string Address1 { get; init; }
        public string? Address2 { get; init; }
        required public string Town { get; init; }
        required public string Postcode { get; init; }
        public string? Country { get; init; }
    }

    public class User : Person
    {
        public bool HasImage { get; init; }
        public string? ImageUrl { get; init; }
        required public NextOfKin NextOfKin { get; init; }
        public string? NationalInsuranceNumber { get; init; }
        public Vehicle? Vehicle { get; init; }
    }

    public class Vehicle
    {
        required public string Registration { get; init; }
        public string? Make { get; init; }
        public string? Model { get; init; }
        public string? Colour { get; init; }
        public string? Notes { get; init; }
    }

    public class NextOfKin : Person
    {
        required public string Relationship { get; init; }
    }


    [ApiController]
    [Route("users")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new List<User>
        {
            new() { Id = new Guid("BD164D33-EDAE-4990-A58A-196024780654"), Title="Mr" , FirstName = "Alan", LastName = "Swamble", DateOfBirth = new DateOnly(1964, 5, 15),
                HasImage = true,
                ImageUrl="https://noktemp.blob.core.windows.net/images/BD164D33-EDAE-4990-A58A-196024780654.png",
                NationalInsuranceNumber = "AB123456C",
                Address = new Address
                {
                    Address1 = "1 The Street",
                    Town = "The Town",
                    Postcode = "AB1 2CD"
                },

                Vehicle = new Vehicle
                {
                    Registration = "AB12 CDE",
                    Make = "Ford",
                    Model = "Focus",
                    Colour = "Blue",
                    Notes = "Has a dent in the rear bumper"
                },
                NextOfKin = new NextOfKin{
                    Id = new Guid("4BD9324E-ACFB-4BC2-9DFB-FE429826E0E8"),
                    FirstName = "John",
                    LastName = "Swamble",
                    Email = "john@swamble.com",
                    Phone = "08777775656",
                    Mobile = "029209022022",
                    Address = new Address
                    {
                        Address1 = "1 The Street",
                        Town = "The Town",
                        Postcode = "AB1 2CD"
                    },
                    Relationship = "Brother"
                },
            },
            new() { Id = new Guid("47CFB606-0C1C-4557-9342-538CF3AD3F3B"), Title="Mrs" ,FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1985, 8, 22),
                 HasImage = true,
                ImageUrl="https://noktemp.blob.core.windows.net/images/47CFB606-0C1C-4557-9342-538CF3AD3F3B.png",
                NationalInsuranceNumber = "BB123456D",
                Address = new Address
                {
                    Address1 = "11 Another Street",
                    Town = "A Town",
                    Postcode = "BB6 8UY"
                },
                Vehicle = new Vehicle
                {
                    Registration = "BB12 CDE",
                    Make = "Ford",
                    Model = "Focus",
                    Colour = "Blue",
                    Notes = "Has a dent in the rear bumper"
                },
                NextOfKin = new NextOfKin{
                    Id = new Guid("4F0833F6-82D4-4E7E-839B-4D5EC69CC938"),
                    FirstName = "Stefan",
                    LastName = "Smith",
                    Email = "stefan@smith.com",
                    Phone = "011171181178",
                    Mobile = "09787665432",
                    Address = new Address
                    {
                        Address1 = "11 Another Street",
                        Town = "A Town",
                        Postcode = "BB6 8UY"
                    },
                    Relationship = "Husband"
                }
            },
            new() { Id = new Guid("26432532-8B41-4C45-A3F7-B0DB7F395830"), Title="Miss" ,FirstName = "Sally", LastName = "Smith", DateOfBirth = new DateOnly(1999, 1, 1),
                HasImage = false,
               NationalInsuranceNumber = "CC123456E",
                           NextOfKin = new NextOfKin
                           {
                    Id = new Guid("029073F7-5FFB-42D2-80E0-04948C9A3540"),
                    FirstName = "Sue",
                    LastName = "Smith",
                    Email = "",
                    Phone = "",
                    Mobile = "",
                    Address = new Address
                    {
Address1 = "11 Another Street",
                        Town = "A Town",
                        Postcode = "BB6 8UY"
                    },
                    Relationship = "Mother"
                           }
        }
        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        public class UserListItem
        {
            required public Guid UserId { get; init; }
            required public string FirstName { get; init; }
            required public string LastName { get; init; }
            required public DateOnly? DateOfBirth { get; init; }
            required public string? KnownTown { get; init; }
            required public string? KnownVehicleReg { get; init; }
            required public bool HasImage { get; init; }
        }

        [HttpGet]
        public IEnumerable<UserListItem> GetUsers([FromQuery] string? searchTerm = null)
        {
            IEnumerable<User> x;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                x=Users.Where(u => u.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || u.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                x = Users;
            }

            return x.Select(u => new UserListItem
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                KnownTown = u.Address?.Town,
                KnownVehicleReg = u.Vehicle?.Registration,
                HasImage = u.HasImage
            });
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(Guid userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return user;
        }
    }
}
