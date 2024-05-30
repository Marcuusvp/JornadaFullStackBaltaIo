using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class CategoriesEndpoints : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", CreateAsync).WithName("Categories: Create").WithSummary("Cria uma nova categoria").WithOrder(1).Produces<Response<Category?>>();
            app.MapDelete("/{id}", DeleteAsync).WithName("Categories: Delete").WithSummary("Delete uma categoria").WithOrder(3).Produces<Response<Category?>>();
            app.MapGet("/", GetAllAsync).WithName("Categories: Get All").WithSummary("Obtem todas categorias").WithOrder(2).Produces<Response<Category?>>();
            app.MapGet("/{id}", GetByIdAsync).WithName("Categories: Get by id").WithSummary("Obtem uma categoria").WithOrder(4).Produces<Response<Category?>>();
            app.MapPut("/{id}", UpdateAsync).WithName("Categories: Update").WithSummary("Atualiza uma categoria").WithOrder(5).Produces<Response<Category?>>();
        }

        private static async Task<IResult> CreateAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
                : TypedResults.BadRequest();
        }

        private static async Task<IResult> DeleteAsync(ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> GetAllAsync(ICategoryHandler handler,
         [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
         [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> GetByIdAsync(ICategoryHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> UpdateAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}