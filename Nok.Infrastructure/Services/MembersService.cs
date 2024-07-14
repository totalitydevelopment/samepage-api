using Microsoft.EntityFrameworkCore;
using Nok.Core.Aggregates.Register;
using Nok.Core.Interfaces;
using Nok.Core.Models;
using Nok.Infrastructure.Data;
using Name = Nok.Core.Aggregates.Register.Name;

namespace Nok.Infrastructure.Services;

public class MembersService : IMembersService
{
    //TODO Split this into a readonly interface for the GETS

    private readonly DatabaseContext _databaseContext;

    public MembersService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Guid> CreateMemberAsync(CreateMember newMember)
    {
        var member = new Member(Guid.NewGuid(), new Name(newMember.Title, newMember.FirstName, newMember.MiddleName, newMember.LastName));

        if (!string.IsNullOrEmpty(newMember.Email))
        {
            member.SetContactEmail(newMember.Email);
        }

        _databaseContext.Members.Add(member);
        await _databaseContext.SaveChangesAsync();

        return member.Id;
    }

    public async Task<GetMember?> GetMemberAsync(Guid id)
    {
        var member = await _databaseContext.Members
           .Include(x => x.NextOfKins)
           .FirstOrDefaultAsync(x => x.Id == id);

        if (member == null)
            return null;

        return new GetMember
        {
            Id = member.Id,
            Name = new Name(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
            Contact = member.Contact == null ? null : new Contact(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber),
            Vehicle = member.Vehicle == null ? null : new Vehicle(member.Vehicle.RegistrationNumber, member.Vehicle.Make, member.Vehicle.Model, member.Vehicle.Colour, member.Vehicle.Notes),
            HasImage = member.HasImage,
            DateOfBirth = member.DateOfBirth == null ? null : new DateOfBirth(member.DateOfBirth.Year, member.DateOfBirth.Month, member.DateOfBirth.Day),
            NextOfKins = member.NextOfKins.Select(member => new NextOfKin(member.Id, new Name(member.Name.Title, member.Name.FirstName, member.Name.MiddleName, member.Name.Surname),
            new ContactDetails(member.Contact.Email, member.Contact.HomeNumber, member.Contact.WorkNumber, member.Contact.MobileNumber), member.Relationship)).ToList(),
            ImageUrl = member.HasImage ? "https://noktemp.blob.core.windows.net/images/" + member.ImageUrl : string.Empty
        };
    }

    public async Task<IEnumerable<GetMemberListItem>> GetListAsync(string? searchTerm = null)
    {
        IEnumerable<Member> members = _databaseContext.Members;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            members = members.Where(u => u.Name.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || u.Name.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult<IEnumerable<GetMemberListItem>>(members.Select(u => new GetMemberListItem
        {
            Id = u.Id,
            Name = new Name(u.Name.Title, u.Name.FirstName, u.Name.MiddleName, u.Name.Surname),
            DateOfBirth = u.DateOfBirth == null ? null : new DateOfBirth(u.DateOfBirth.Year, u.DateOfBirth.Month, u.DateOfBirth.Day),
            KnownTown = u.Address?.Town,
            HasImage = u.HasImage
        }).ToList());
    }
}
