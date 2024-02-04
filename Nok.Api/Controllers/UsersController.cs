//using Microsoft.AspNetCore.Mvc;
//using Nok.Core.Aggregates.Register;
//using Nok.Infrastructure.Data;

//namespace Nok.Api.Controllers;


///// <summary>
///// This controller is temporary. The demo mobile police app is using it but it will be removed
///// </summary>
//[ApiController]
//[Route("users")]
////[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
//public class UsersController : ControllerBase
//{

//    private readonly ILogger<UsersController> _logger;
//    private readonly DatabaseContext _databaseContext;

//    public UsersController(ILogger<UsersController> logger, DatabaseContext databaseContext)
//    {
//        _logger = logger;
//        _databaseContext = databaseContext;
//    }

//    [HttpGet]
//    public IEnumerable<UserListItem> GetUsers([FromQuery] string? searchTerm = null)
//    {
//        IEnumerable<Member> members = _databaseContext.Members;

//        if (!string.IsNullOrWhiteSpace(searchTerm))
//        {
//            members = members.Where(u => u.Name.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || u.Name.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
//        }

//        return members.Select(u => new UserListItem
//        {
//            UserId = u.Id,
//            FirstName = u.Name.FirstName,
//            LastName = u.Name.Surname,
//            DateOfBirth = u.DateOfBirth == null ? null : new DateOnly(u.DateOfBirth.Year, u.DateOfBirth.Month.Value, u.DateOfBirth.Day.Value),
//            KnownTown = u.Address?.Town,
//            KnownVehicleReg = u.Vehicle?.RegistrationNumber,
//            HasImage = u.HasImage
//        });
//    }

//    [HttpGet("{userId}")]
//    public ActionResult<dynamic> GetUserById(Guid userId)
//    {
//        var member = _databaseContext.Members.Find(userId);

//        if (member == null)
//        {
//            return NotFound();
//        }

//        return new
//        {
//            Id = member.Id,

//            Title = member.Name.Title,
//            FirstName = member.Name.FirstName,
//            MiddleName = member.Name.MiddleName,
//            LastName = member.Name.Surname
//            Contact = member.Contact == null ? null : new
//            {
//                Email = member.Contact.Email,
//                HomeNumber = member.Contact.HomeNumber,
//                WorkNumber = member.Contact.WorkNumber,
//                MobileNumber = member.Contact.MobileNumber
//            },
//            Vehicle = member.Vehicle == null ? null : new
//            {
//                RegistrationNumber = member.Vehicle.RegistrationNumber,
//                Make = member.Vehicle.Make,
//                Model = member.Vehicle.Model,
//                Colour = member.Vehicle.Colour,
//                Notes = member.Vehicle.Notes
//            },
//            HasImage = member.HasImage,
//            DateOfBirth = member.DateOfBirth == null ? null : new DateOnly(member.DateOfBirth.Year, member.DateOfBirth.Month.Value, member.DateOfBirth.Day.Value),
//            ImageUrl = member.ImageUrl
//        };


//        //new()
//        //{
//        //    Id = new Guid("47CFB606-0C1C-4557-9342-538CF3AD3F3B"),
//        //    Title = "Mrs",
//        //    FirstName = "Jane",
//        //    LastName = "Smith",
//        //    DateOfBirth = new DateOnly(1985, 8, 22),
//        //    HasImage = true,
//        //    ImageUrl = "https://noktemp.blob.core.windows.net/images/47CFB606-0C1C-4557-9342-538CF3AD3F3B.png",
//        //    NationalInsuranceNumber = "BB123456D",
//        //    Address = new Address
//        //    {
//        //        Address1 = "11 Another Street",
//        //        Town = "A Town",
//        //        Postcode = "BB6 8UY"
//        //    },
//        //    Vehicle = new Vehicle
//        //    {
//        //        Registration = "BB12 CDE",
//        //        Make = "Ford",
//        //        Model = "Focus",
//        //        Colour = "Blue",
//        //        Notes = "Has a dent in the rear bumper"
//        //    },
//        //    NextOfKin = new NextOfKin
//        //    {
//        //        Id = new Guid("4F0833F6-82D4-4E7E-839B-4D5EC69CC938"),
//        //        FirstName = "Stefan",
//        //        LastName = "Smith",
//        //        Email = "stefan@smith.com",
//        //        Phone = "011171181178",
//        //        Mobile = "09787665432",
//        //        Address = new Address
//        //        {
//        //            Address1 = "11 Another Street",
//        //            Town = "A Town",
//        //            Postcode = "BB6 8UY"
//        //        },
//        //        Relationship = "Husband"
//        //    };
//        //return new GetMemberResponse
//        //{
//        //    Id = member.Id,
//        //    Name = new NameResponse(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
//        //    Contact = member.Contact == null ? null : new ContactResponse(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber),
//        //    Vehicle = member.Vehicle == null ? null : new VehicleResponse(member.Vehicle.RegistrationNumber, member.Vehicle.Make, member.Vehicle.Model, member.Vehicle.Colour, member.Vehicle.Notes)
//        //};
//    }
//}
