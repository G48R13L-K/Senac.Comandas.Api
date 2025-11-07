using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        static List<Comanda> Comandas = new List<Comanda>();
        
            public ComandaDbContext _context { get; set; }

        public ComandaController(ComandaDbContext context)
        {
            _context = context;
        }
        


// GET: api/<ComandaController>
[HttpGet]
        public IResult Get()
        {
            var comandas = _context.Comandas.ToList();
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var Comanda = _context.Comandas.FirstOrDefault(x => x.Id == id);
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
                
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
                
                

            };
            //cria variavel do tipo lista de items
            var comandaItens = new List<ComandaItem>();

            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {//atribui os itens a nota comanda
                var comandaItem = new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    Comanda = novaComanda,
                }; 
                comandaItens.Add(comandaItem);
                var cardapioItem = _context.cardapioItems.FirstOrDefault(c => c.Id == cardapioItemId);
                if (cardapioItem!.PossuiPreparo)
                {
                    var pedido = new PedidoCozinha
                    {
                        Comanda = novaComanda
                    };
                    var pedidoItem = new PedidoCozinhaItem
                    {
                        ComandaItem = comandaItem,
                        PedidoCozinha = pedido

                    };
                    _context.pedidoCozinhas.Add(pedido);
                    _context.pedidoCozinhaItems.Add(pedidoItem);
                }
            }
            //adiciona a nova comanda a lista de comandas
            novaComanda.Itens = comandaItens;
            _context.Comandas.Add(novaComanda);
            _context.SaveChanges();

            var resposta = new ComandaCreateResponse
            {
                Id = novaComanda.Id,
                NomeCliente = novaComanda.NomeCliente,
                NumeroMesa = novaComanda.NumeroMesa,
                Itens = novaComanda.Itens.Select(i => new ComandaItemResponse
                {
                    id = i.Id,
                    Titulo = _context.cardapioItems.First(ci => ci.Id == i.CardapioItemId).Titulo
                }).ToList()
            };
            return Results.Created($"/api/comanda/{resposta.Id}", resposta); 
            }
                
            
          

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente dete ter no minimo 3 caracteres.");
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
           



            var comanda =  _context.Comandas.FirstOrDefault(u => u.Id == id);
            if (comanda is null)
                return Results.NotFound($"Comanda{id} não encontrada!");
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            _context.Comandas.Add(comanda);
            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada");
            _context.Comandas.Remove(comanda);
            var comandaRemovida = _context.SaveChanges() ;
            if (comandaRemovida>0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
