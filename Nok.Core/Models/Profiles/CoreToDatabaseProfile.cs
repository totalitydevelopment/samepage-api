using AutoMapper;
using Nok.Core.Aggregates.Register;
using Nok.Core.Models;

namespace Nok.Core.Models.Profiles;

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

        CreateMap<NextOfKinDto, NextOfKin>();
        CreateMap<NextOfKin, NextOfKinDto>();

        CreateMap<MemberDto, Member>();
        CreateMap<Member, MemberDto>();
    }
}
