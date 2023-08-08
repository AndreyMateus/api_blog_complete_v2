using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using Blog.Models;
using blog.ViewModels;

namespace blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    // Versionamento de API - nos possibilita que quem não tenha atualizado o APP - Ainda consiga utilizar a versão antiga.
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] AppDBContext appDBContext)
    {
        try
        {
           var listReturned =  await appDBContext.Categories.ToListAsync();

            return Ok(new ResultViewModel<List<Category>>(listReturned));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("Erro 12 - Falha Interna no Servidor"));
        }
    }

    [HttpGet("v1/category/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDBContext appDBContext, [FromRoute] int id)
    {
        // Com o AWAIT ele vai iniciar o processamento e ficar VIGIANDO e esperando ele terminar de processar, isso faz com que se tivessemos outros métodos abaixo como estamos simulando com o método 2 e 3, eles seriam executados em seguida sem que tivessem que esperar esse primeiro terminar, só esperariam caso fossem utilizar a variável category.
        try
        {
            var category = await appDBContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<Category>(category));

            // método 2 - simulação
            // método 3 - simulação 
        }
        catch (DbUpdateException dbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Category>("Erro 14 - Não foi possível inserir uma nova categoria."));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Category>("Erro 15 - Não foi possível inserir uma nova categoria."));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromServices] AppDBContext appContext, [FromBody]EditorCategoryViewModel model)
    {
        try
        {
            // Por padrão o aspnet já faz isso para nós, mas se quisermos explicitar isso ou alterar um pouco do processso, precisamos utilizar o ModelState.
            // No nosso casso iremos desabilitar a validação automática, pois criaremos a nossa e padronizaremos ela, para isso precisaremos obrigatoriamente desse trecho de código.
            if(!ModelState.IsValid)
            return BadRequest();

            var category = new Category()
            {
                // se o Id for igual a 0, é entendido que é um novo REGISTRO.
                Id = 0,
                Name = model.Name,
                // Slug = model.Name.ToLower()
                Slug = model.Slug,
            };

            await appContext.Categories.AddAsync(category);
            await appContext.SaveChangesAsync();

            // URL onde o objeto criado estará diponível para consulta.
            return Created($"v1/category/{category.Id}", category);
        }
        catch (DbUpdateException dbUpdateException)
        {
            return StatusCode(500, "Erro 14 - Não foi possível inserir uma nova categoria.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro 15 - Não foi possível inserir uma nova categoria.");
        }
    }

    [HttpPut("v1/category/{id:int}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDBContext appDBContext, [FromRoute] int id, [FromBody] EditorCategoryViewModel category)
    {
        try
        {
            var categoryReturned = await appDBContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (categoryReturned is null)
                return NotFound();

            categoryReturned.Name = category.Name;
            categoryReturned.Slug = category.Slug;

            appDBContext.Update(categoryReturned);
            await appDBContext.SaveChangesAsync();
            return Ok(categoryReturned);
        }
        catch (DbUpdateException dbUpdateException)
        {
            return StatusCode(500, "Erro 14 - Não foi possível inserir uma nova categoria.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro 15 - Não foi possível inserir uma nova categoria.");
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDBContext appDBContext, [FromRoute] int id)
    {
        try
        {
            var categoryReturned = await appDBContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (categoryReturned is null)
                return NotFound();

            appDBContext.Categories.Remove(categoryReturned);
            await appDBContext.SaveChangesAsync();

            return Ok(categoryReturned);
        }
        catch (DbUpdateException dbUpdateException)
        {
            return StatusCode(500, "Erro 14 - Não foi possível inserir uma nova categoria.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro 15 - Não foi possível inserir uma nova categoria.");
        }
    }

}
