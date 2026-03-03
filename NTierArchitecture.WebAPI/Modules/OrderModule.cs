using Carter;
using NTierArchitecture.Business.Services;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.WebAPI.Fliters;

namespace NTierArchitecture.WebAPI.Modules
{
    public sealed class OrderModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder group)
        {
            var app = group.MapGroup("/orders").WithTags("Orders");

            app.MapPost(string.Empty, async (
                OrderCreateDTO request,
                OrderService service,
                CancellationToken cancellationToken) =>
            {
                await service.CreateAsync(request, cancellationToken);
                return Results.Created();
            }).AddEndpointFilter<ValidationFilter<OrderCreateDTO>>(); 

            app.MapGet("/{id}", async (
                Guid id,
                OrderService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.GetAsync(id,cancellationToken);
                return Results.Ok(res);
            }).Produces<Order>(); ;

            app.MapGet(string.Empty, async (
                OrderService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.GetAllAsync(cancellationToken);
                return Results.Ok(res);
            }).Produces<List<Order>>();

            app.MapPut(string.Empty, async (
                OrderUpdateDTO request,
                OrderService service,
                CancellationToken cancellationToken) =>
            {
                await service.UpdateAsync(request, cancellationToken);
                return Results.Ok();
            }).AddEndpointFilter<ValidationFilter<OrderUpdateDTO>>();

            app.MapDelete("/{id}", async (
               Guid id,
               OrderService service,
               CancellationToken cancellationToken) =>
            {
                await service.DeleteAsync(id, cancellationToken);
                return Results.Ok();
            });
        }
    }
}
