using Microsoft.Extensions.DependencyInjection;
using GuacAPI.Repositories;
using GuacAPI.Interface;


//DIMethod for Dependancy Injection Method

namespace GuacAPI.ExtensionMethods;

public static class DIMethods
{

    #region Public methods
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, DefaultProductRepository>();
        }
    #endregion

}