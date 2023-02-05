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
        // CreateMap<UpdateRequest, User>()
        //     .ForAllMembers(x => x.Condition(
        //         (src, dest, prop) =>
        //         {
        //             // ignore null & empty string properties
        //             if (prop == null) return false;
        //             if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        //             return true;
        //         }
        //     ));
    }
}