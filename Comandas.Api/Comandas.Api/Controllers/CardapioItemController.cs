using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
        // GET: api/<CardapioItemController>

        public List<CardapioItem> cardapios = new List<CardapioItem>(); 
            
            public ComandaDbContext _context{get; set;}

        public CardapioItemController(ComandaDbContext context)
        {
            _context = context;
        }
        

[HttpGet]
        public IResult GetCardapios()
        {
            var cardapios = _context.cardapioItems.Include(c => c.CategoriaCardapio).ToList();
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {

            //Buscar na lista de cardapios de acordo com o id
            var cardapio = _context.cardapioItems
                .Include(ci => ci.CategoriaCardapioId)
                .FirstOrDefault(c => c.Id == id);
            if (cardapio == null)
            {
                return Results.NotFound("Cardápio não encontrado");
            }
            else
            {
                return Results.Ok(cardapio);
            }
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
                return Results.BadRequest("O titulo deve possuir mais de 3 caracteres.");
            if (cardapio.Descricao.Length < 3)
                return Results.BadRequest("A descrição do item deve possuir mais de 3 caracteres.");
            if (cardapio.Preco <= 0)
                return Results.BadRequest("O preço deve ser maior que zero.");
            //validaçãp de categoria
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _context.CategoriaCardapio.FirstOrDefault
                    (c => c.Id == cardapio.CategoriaCardapioId);
                if (categoria is null) {
                    return Results.BadRequest("Categoria de cardápio inválida.");
                }
            }
            var cardapioItem = new CardapioItem
            {
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
                Imagem = cardapio.Imagem,
                CategoriaCardapioId = cardapio.CategoriaCardapioId
            };

            _context.cardapioItems.Add(cardapioItem);
            _context.SaveChanges();
            return Results.Created($"/api/cardapioitem/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var CardapioItem = _context.cardapioItems.FirstOrDefault(u => u.Id == id);
            if (CardapioItem is null)
                return Results.NotFound($"Cardapio {id} não foi encontrado.");
            //se categoria for informada
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _context.CategoriaCardapio.FirstOrDefault
                    (c => c.Id == cardapio.CategoriaCardapioId);
                if (categoria is null)
                {
                    return Results.BadRequest("Categoria de cardápio inválida.");
                }
            }

            CardapioItem.Titulo =cardapio.Titulo;
            CardapioItem.Descricao = cardapio.Descricao;
            CardapioItem.Preco = cardapio.Preco;
            CardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            CardapioItem.Imagem = cardapio.Imagem;
            CardapioItem.CategoriaCardapioId = cardapio.CategoriaCardapioId;
            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = _context.cardapioItems.FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound("Item do cardápio não encontrada");
             _context.cardapioItems.Remove(cardapioItem);
            var itemRemovido = _context.SaveChanges();
            if (itemRemovido > 0)
            {
                return Results.NoContent();
                
            }

            return Results.StatusCode(500);
        }

    }
}
