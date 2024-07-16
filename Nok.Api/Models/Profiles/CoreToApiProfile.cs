using AutoMapper;
using Nok.Api.Controllers;
using Nok.Core.Aggregates.Register;
using Nok.Core.Models;

namespace Nok.Api.Models.Profiles;

public class CoreToApiProfile : Profile
{
    public CoreToApiProfile()
    {
        // Outbound from Core to API
        CreateMap<GetMember, GetMemberResponse>();
        CreateMap<Name, NameResponse>().ReverseMap();
        CreateMap<Contact, ContactResponse>().ReverseMap();
        CreateMap<Vehicle, VehicleResponse>().ReverseMap();
        CreateMap<DateOfBirth, DateOfBirthResponse>().ReverseMap();
        CreateMap<NextOfKin, NextOfKinResponse>();

        // Inbound from API to Core
    }
}
