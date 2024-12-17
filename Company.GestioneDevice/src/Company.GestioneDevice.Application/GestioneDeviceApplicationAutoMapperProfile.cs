using AutoMapper;
using Company.GestioneDevice.Policies;
using Company.GestioneDevice.Users;

namespace Company.GestioneDevice;

public class GestioneDeviceApplicationAutoMapperProfile : Profile
{
    public GestioneDeviceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Policies, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.UserPolicies, opt => opt.Ignore());


        CreateMap<User, CreateUserDto>()
            .ForMember(dest => dest.PolicyIds, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.UserPolicies, opt => opt.Ignore());


        CreateMap<Policy, PolicyDto>()
            .ReverseMap();


    }
}
