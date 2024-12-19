using AutoMapper;
using Company.GestioneDevice.Devices;
using Company.GestioneDevice.Devices.DeviceFeatures;
using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;
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
            .ReverseMap();

        CreateMap<User, UserDetailedDto>()
           .ReverseMap();

        CreateMap<User, CreateUserDto>()
            .ForMember(dest => dest.PolicyIds, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.UserPolicies, opt => opt.Ignore());

        CreateMap<User, UserLookupDto>()
            .ReverseMap();



        CreateMap<Policy, PolicyDto>()
            .ReverseMap();



        CreateMap<Device, DeviceDto>()
            .ReverseMap();

        CreateMap<Device, DeviceDetailedDto>()
            .ReverseMap();

        CreateMap<Device, CreateDeviceDto>()
           .ForMember(dest => dest.DeviceFeaturesIds, opt => opt.Ignore())
           .ReverseMap()
           .ForMember(dest => dest.DeviceFeatures, opt => opt.Ignore());



        CreateMap<Feature, FeatureDto>()
           .ReverseMap();




        CreateMap<SoftwareVersion, SoftwareVersionDto>()
           .ReverseMap();

        CreateMap<SoftwareVersion, SoftwareVersionLookupDto>()
         .ReverseMap();

    }
}
