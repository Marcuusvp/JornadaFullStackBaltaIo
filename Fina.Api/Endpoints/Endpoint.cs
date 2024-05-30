using Fina.Api.Common.Api;
using Fina.Api.Endpoints.Categories;
using Fina.Api.Endpoints.Transactions;

namespace Fina.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endPoints = app.MapGroup("");

            endPoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new { message = "OK" });

            endPoints.MapGroup("v1/categories").WithTags("Categories").MapEndpoint<CategoriesEndpoints>();
            endPoints.MapGroup("v1/transactions").WithTags("Transactions").MapEndpoint<TransactionsEndpoints>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}