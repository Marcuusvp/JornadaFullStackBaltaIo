using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions
{
    public class TransactionsEndpoints : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", CreateAsync).WithName("Categories: Create").WithSummary("Cria uma nova transação").WithOrder(1).Produces<Response<Transaction?>>();
            app.MapDelete("/{id}", DeleteAsync).WithName("Categories: Delete").WithSummary("Delete uma transação").WithOrder(3).Produces<Response<Transaction?>>();
            app.MapGet("/", GetAllAsync).WithName("Categories: Get By Period").WithSummary("Obtem todas transações em um periodo de tempo").WithOrder(2).Produces<Response<Transaction?>>();
            app.MapGet("/{id}", GetByIdAsync).WithName("Categories: Get by id").WithSummary("Obtem uma transação").WithOrder(4).Produces<Response<Transaction?>>();
            app.MapPut("/{id}", UpdateAsync).WithName("Categories: Update").WithSummary("Atualiza uma transação").WithOrder(5).Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> CreateAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
                : TypedResults.BadRequest();
        }

        private static async Task<IResult> DeleteAsync(ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> GetAllAsync(ITransactionHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                StartDate = startDate,
                EndDate = endDate,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await handler.GetByPeriod(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> GetByIdAsync(ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> UpdateAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
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