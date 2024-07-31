using AutoMapper;
using SamePage.Core.Aggregates.Register;

namespace SamePage.Core.Models.Profiles;

public class CoreToDatabaseProfile : Profile
{
    public CoreToDatabaseProfile()
    {
        CreateMap<NameDto, Name>();
        CreateMap<Name, NameDto>();

        CreateMap<ContactDetailsDto, ContactDetails>();
        CreateMap<ContactDetails, ContactDetailsDto>();

        CreateMap<VehicleDto, Vehicle>();
        CreateMap<Vehicle, VehicleDto>();

        CreateMap<DateOfBirthDto, DateOfBirth>();
        CreateMap<DateOfBirth, DateOfBirthDto>();

        CreateMap<AddressDto, Address>();
        CreateMap<Address, AddressDto>();

        CreateMap<NextOfKinRequest, NextOfKin>();
        CreateMap<NextOfKinRequestWithId, NextOfKin>();
        CreateMap<NextOfKin, NextOfKinResponse>();

        CreateMap<MemberRequest, Member>();
        CreateMap<MemberRequestWithId, Member>();
        CreateMap<Member, MemberResponse>();
    }
}
