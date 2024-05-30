using Fina.Api.Data.Mappings;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };
            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, 201, "Categoria criada com sucesso");
            } 
            catch (Exception ex) 
            {
                // Serilog
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, 500, "Não foi possível criar categoria");
                throw;
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria removida");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, 500, "OCorreu um erro ao excluir categoria");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
        {
            try 
            {
                var query = context
                                .Categories
                                .AsNoTracking()
                                .Where(x => x.UserId == request.UserId)
                                .OrderBy(x => x.Title);
                
                var categories = await query
                                    .Skip((request.PageNumber - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PagedResponse<List<Category>?>(null, 500, "Não foi possível encontrar categorias");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                return category is null ? new Response<Category?>(null, 404, "Não foi possível encontrar a categoria")
                    : new Response<Category?>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, 500, "Erro ao encontrar categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category is null)
                {
                    return new Response<Category?>(null, 404,"Categoria não encontrada");
                }
                category.Title = request.Titulo;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria Atualizada");         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, 500, "Ocorreu um erro ao atualizar categoria");
            }
        }
    }
}