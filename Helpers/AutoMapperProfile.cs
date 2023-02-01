namespace GuacAPI.Helpers;

using AutoMapper;
using GuacAPI.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse 
        CreateMap<User, UserReturnDto>();

        // RegisterRequest -> User
        CreateMap<UserDtoRegister, User>();

        // UpdateRequest -> User
        CreateMap<UserDtoUpdate, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}