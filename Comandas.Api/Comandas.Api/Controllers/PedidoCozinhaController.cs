using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        public ComandaDbContext _context { get; set; }

        public PedidoCozinhaController(ComandaDbContext context)
        {
            _context = context;
        }



        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            var PedidoCozinha = _context.pedidoCozinhas.ToList();
            return Results.Ok(PedidoCozinha);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var PedidoCozinha = _context.pedidoCozinhas.FirstOrDefault(x => x.Id == id);
            if (PedidoCozinha == null)
            {
                return Results.NotFound("Pedido não encontrado.");
            }
            else
            {
                return Results.Ok(PedidoCozinha);
            }
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        
        public IResult Post([FromBody] PedidoCozinhaCreateRequest pedidocozinhaCreate)
        {
            var novoPedidoCozinha = new PedidoCozinha
            {

                ComandaId = pedidocozinhaCreate.ComandaId,

            };
            var pedidosItem = new List<PedidoCozinhaItem>();
            foreach (var item in pedidocozinhaCreate.items)
            {
                var pedidoItem = new PedidoCozinhaItem
                {

                    ComandaItemId = item.ComandaItemId,
                    PedidoCozinhaId = item.PedidoCozinhaId
                };
                pedidosItem.Add(pedidoItem);
            }

            novoPedidoCozinha.Itens = pedidosItem;

            _context.pedidoCozinhas.Add(novoPedidoCozinha);
            _context.SaveChanges();
            return Results.Created($"/api/PedidoCozinha/{novoPedidoCozinha.Id}", novoPedidoCozinha);

        }
    
        

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoCozinhaUpdate)
        {
            var pedidoCozinha = _context.pedidoCozinhas.FirstOrDefault(x => x.Id == id);
            if (pedidoCozinha == null)
            {
                return Results.NotFound("Pedido não encontrado.");
            }
            foreach (var item in pedidoCozinhaUpdate.items)
            {
                var pedidoItem = new PedidoCozinhaItem
                {

                    ComandaItemId = item.ComandaItemId,
                    PedidoCozinhaId = item.PedidoCozinhaId
                };
                _context.pedidoCozinhas.Add(pedidoCozinha);
                _context.SaveChanges();
            }
                return Results.NoContent();

            

        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
