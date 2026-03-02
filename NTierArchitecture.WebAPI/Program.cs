using Carter;
using NTierArchitecture.Business;
using NTierArchitecture.DataAccess;
using Scalar.AspNetCore;

namespace NTierArchitecture.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddBusiness();
            builder.Services.AddCarter();
            builder.Services.AddOpenApi();

            var app = builder.Build();
            
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.MapCarter();
            app.Run();
        }
    }
}
