using GuacAPI.Entities;
using GuacAPI.Models.Users;

namespace GuacAPI.Helpers;

using AutoMapper;
using GuacAPI.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse 
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

        // UpdateRequest -> User
        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));


        CreateMap<InvoiceFurnisherRegister, InvoiceFurnisher>();
        CreateMap<InvoiceFurnisherUpdate, InvoiceFurnisher>()
            .ForMember(dest => dest.FurnisherId, opt =>
            {
                opt.Condition((src, dest) => dest.FurnisherId == 0);// suppose dest is not null.
                opt.MapFrom(src => src.FurnisherId);

            });
    }
}