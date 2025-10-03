using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        public List<PedidoCozinha> PedidosCozinha = new List<PedidoCozinha>()
        {
            new PedidoCozinha {
                Id = 1, 
                ComandaId = 1,
                
            },
            new PedidoCozinha
            {
                Id = 2,
                ComandaId = 2,
            }

        };
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(PedidosCozinha);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var PedidoCozinha = PedidosCozinha.FirstOrDefault(x => x.Id == id);
            if (PedidoCozinha == null)
            {
                return Results.NotFound("Pedido não encontrado.");
            }
            else
            {
                return Results.Ok(PedidosCozinha);
            }
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
