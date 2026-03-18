using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.Business.Services;

namespace NTierArchitecture.Business
{
    public static class BusinessRegistrar
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<CategoryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<OrderService>();
            services.AddValidatorsFromAssembly(typeof(BusinessRegistrar).Assembly);
        }
    }
}
