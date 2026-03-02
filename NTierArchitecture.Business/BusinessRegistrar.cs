using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business
{
    public static class BusinessRegistrar
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<CategoryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<OrderService>();
        }
    }
}
