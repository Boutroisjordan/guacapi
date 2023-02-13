using GuacAPI.Entities;
using GuacAPI.Models.Users;

namespace GuacAPI.Helpers;

using AutoMapper;
using GuacAPI.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        CreateMap<ProductRegister, Product>();
        CreateMap<AlcoholTypeRegister, AlcoholType>();
        CreateMap<CommentRegister, Comment>();
        CreateMap<DomainRegister, Domain>();
        CreateMap<FurnisherRegister, Furnisher>();

        // User -> AuthenticateResponse 
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

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

        // CreateMap<OrderRegistryDTO, Order>();
        CreateMap<OrderRegistryDTO, Order>()
            .ForMember(dest => dest.OrderOffers, opt => opt.MapFrom(src => src.OrderOfferRegistryDTOs));

        CreateMap<OrderUpdateDTO, Order>()
            .ForMember(dest => dest.OrderOffers, opt => opt.MapFrom(src => src.OrderOfferRegistryDTOs));

        CreateMap<InvoiceFurnisherRegister, InvoiceFurnisher>()
            .ForMember(dest => dest.InvoicesFurnisherProduct, opt => opt.MapFrom(src => src.InvoicesFurnisherProductRegister));

        CreateMap<InvoiceFurnisherUpdate, InvoiceFurnisher>()
            .ForMember(dest => dest.InvoicesFurnisherProduct, opt => opt.MapFrom(src => src.InvoicesFurnisherProductRegister));

        CreateMap<OfferRegister, Offer>()
            .ForMember(dest => dest.ProductOffers, opt => opt.MapFrom(src => src.ProductOffersRegister));

        CreateMap<OrderOfferRegistryDTO, OrderOffer>();
        CreateMap<InvoiceFurnisherProductRegister, InvoiceFurnisherProduct>();
        CreateMap<OrderOfferRegistryDTO, OrderOffer>();
        CreateMap<ProductOfferRegister, ProductOffer>();
    }
}