using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
        // GET: api/<CardapioItemController>
        
        public List<CardapioItem> cardapios = new List<CardapioItem>() {
            new CardapioItem
            {       Id = 1,
                    Titulo = "Coxinha",
                    Descricao = "Deliciosa coxinha de frango",
                    Preco = 5.00M,
                    PossuiPreparo = true },
            new CardapioItem
            {       Id = 2,
                    Titulo = "Coca-Cola",
                    Descricao = "Lata de refrigerante 350ml",
                    Preco = 4.00M,
                    PossuiPreparo = false }
        
        };

        [HttpGet]
        public IResult GetCardapios()
        {
            
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {

            //Buscar na lista de cardapios de acordo com o id
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
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
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
            };
            cardapios.Add(cardapioItem);
            return Results.Created($"/api/cardapioitem/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var CardapioItem = cardapios.FirstOrDefault(u => u.Id == id);
            if (CardapioItem is null)
                return Results.NotFound($"Cardapio {id} não foi encontrado.");

            CardapioItem.Titulo =cardapio.Titulo;
            CardapioItem.Descricao = cardapio.Descricao;
            CardapioItem.Preco = cardapio.Preco;
            CardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound("Item do cardápio não encontrada");
            var itemRemovido = cardapios.Remove(cardapioItem);
            if (itemRemovido)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    
    }
}
