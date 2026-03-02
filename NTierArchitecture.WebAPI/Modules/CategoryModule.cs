using Carter;
using Microsoft.AspNetCore.Http;
using NTierArchitecture.Business.Services;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System.Text.RegularExpressions;

namespace NTierArchitecture.WebAPI.Modules
{
    public sealed class CategoryModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder group)
        {
            var app = group.MapGroup("/categories").WithTags("Categories");

            app.MapPost(string.Empty, async (
                CategoryCreateDTO request,
                CategoryService service,
                CancellationToken cancellationToken) =>
            {
                await service.CreateAsync(request, cancellationToken);
                return Results.Created();
            });

            app.MapGet("/{id}", async (
                Guid id,
                CategoryService service,
                CancellationToken cancelation) =>
            {
                var res = await service.GetAsync(id, cancelation);
                return Results.Ok(res);
            }).Produces<Category>();

            app.MapGet(string.Empty, async (
                CategoryService service,
                CancellationToken cancellationToken) =>
            {
                var res = await service.GetAllAsync(cancellationToken);
                return Results.Ok(res);
            }).Produces<List<Category>>();

            app.MapPut(string.Empty, async (
               CategoryUpdateDTO request,
               CategoryService service,
               CancellationToken cancellationToken) =>
            {
                await service.UpdateAsync(request, cancellationToken);
                return Results.Created();
            });

            app.MapDelete("/{id}", async (
                Guid id,
                CategoryService service,
                CancellationToken cancelation) =>
            {
                await service.DeleteAsync(id, cancelation);
                return Results.Ok();
            });
        }
    }
}
