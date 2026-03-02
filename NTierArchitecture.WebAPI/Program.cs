using NTierArchitecture.Business;
using NTierArchitecture.DataAccess;

namespace NTierArchitecture.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddBusiness();


            var app = builder.Build();

            app.Run();
        }
    }
}
