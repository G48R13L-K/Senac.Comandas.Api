using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        public List<Comanda> Comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "Gabriel",
                NumeroMesa = 1,

            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Juninho",
                NumeroMesa = 1,
            }
        };
        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(Comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var Comanda = Comandas.FirstOrDefault(x => x.Id == id);
            if (Comanda == null)
            {
                return Results.NotFound("Comanda não encontrada.");
            }
            else
            {
                return Results.Ok(Comanda);
            }
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente dete ter no minimo 3 caracteres.");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comandaCreate.CardapioItemIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio");
            var novaComanda = new Comanda
            {
                Id = Comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
