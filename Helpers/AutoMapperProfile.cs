using AutoMapper;
using GuacAPI.Models;

 
namespace GuacAPI.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse 
        CreateMap<InvoiceFurnisherRegister, InvoiceFurnisher>();


        // RegisterRequest -> User
        // CreateMap<RegisterRequest, User>();

        // UpdateRequest -> User
        // UpdateRequest -> User
        CreateMap<InvoiceFurnisherUpdate, InvoiceFurnisher>()
            .ForMember(dest => dest.FurnisherId, opt =>
            {
                opt.Condition((src, dest) => dest.FurnisherId == 0);// suppose dest is not null.
                opt.MapFrom(src => src.FurnisherId);

            });
            // .ForAllMembers(x => x.Condition(
            //     (src, dest, prop) =>
            //     {
            //         // ignore null & empty string properties
            //         // if(src.FurnisherId == 0) {
            //         //     dest.FurnisherId == prop.FurnisherId;
            //         // }
            //         // if (prop == null) return false;
            //         // if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

            //         return true;
            //     }
            // ));
            
            
    }
}