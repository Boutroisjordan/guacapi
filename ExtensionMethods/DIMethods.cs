using Microsoft.Extensions.DependencyInjection;
using GuacAPI.Services;
using GuacAPI.Services.UserServices;


//DIMethod for Dependancy Injection Method

namespace GuacAPI.ExtensionMethods;
 
public static class DIMethods
{

    #region Public methods
        public static void AddInjections(this IServiceCollection services)
        {
            // services.AddScoped<IProductRepository, DefaultProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFurnisherService, FurnisherService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<IAlcoholService, AlcoholService>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<IAppellationService, AppellationService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IProductOfferService, ProductOfferService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceServiceProduct, InvoiceServiceProduct>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderOfferService, OrderOfferService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }
    #endregion

}