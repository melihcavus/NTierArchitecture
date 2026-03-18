using Carter;
using NTierArchitecture.Business.Services;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.WebAPI.Fliters;

namespace NTierArchitecture.WebAPI.Modules
{
    public sealed class ProductModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder group)
        {
            var app = group.MapGroup("/products").WithTags("Products");

            app.MapPost(string.Empty, async (
                ProductCreateDTO request,
                ProductService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.CreateProduct(request, cancellationToken);
                return Results.Ok(res);
            }).AddEndpointFilter<ValidationFilter<ProductCreateDTO>>(); 

            app.MapGet("/{id}", async (
                Guid id,
                ProductService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.GetAsync(id, cancellationToken);
                return Results.Ok(res);
            }).Produces<Product>();

            app.MapGet(string.Empty, async (
                int pageNumber,
                int pageSize,
                ProductService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.GetAllAsync(pageNumber, pageSize, cancellationToken );
                return Results.Ok(res);
            }).Produces<List<Product>>();

            app.MapPut(string.Empty, async (
                ProductUpdateDTO request,
                ProductService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.UpdateAsync(request,cancellationToken);
                return Results.Ok(res);
            }).AddEndpointFilter<ValidationFilter<ProductUpdateDTO>>(); 

            app.MapDelete("/{id}", async (
                Guid id,
                ProductService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.DeleteAsync(id, cancellationToken);
                return Results.Ok(res);
            });

        }
    }
}

