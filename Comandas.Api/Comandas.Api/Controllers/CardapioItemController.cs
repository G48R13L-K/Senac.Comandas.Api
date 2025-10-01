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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
