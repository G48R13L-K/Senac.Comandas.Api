using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// Namespace e classe permanecem iguais
namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        // Removido: static List<Comanda> Comandas – não é necessário, use apenas o DB via _context
        public ComandaDbContext _context { get; set; }

        public ComandaController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/Comanda (lista todas)
        [HttpGet]
        public IResult Get()
        {
            var comandas = _context.Comandas.Select
                (c => new ComandaCreateResponse
                {
                    Id = c.Id,
                    NomeCliente = c.NomeCliente,
                    NumeroMesa = c.NumeroMesa,
                    Itens = c.Itens.Select(i => new ComandaItemResponse
                    {
                        id = i.Id,
                        Titulo = _context.cardapioItems.First(ci => ci.Id == i.CardapioItemId).Titulo,
                        Preco = _context.cardapioItems.First(pi => pi.Id == i.CardapioItemId).Preco
                    }).ToList(),
                }).ToList();
            
            return Results.Ok(comandas);
        }

        // GET api/Comanda/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comandas.Select
                (c => new ComandaCreateResponse
                {
                    Id = c.Id,
                    NomeCliente = c.NomeCliente,
                    NumeroMesa = c.NumeroMesa,
                    Itens = c.Itens.Select(i => new ComandaItemResponse
                    {
                        id = i.Id,
                        Titulo = _context.cardapioItems.First(ci => ci.Id == i.CardapioItemId).Titulo,
                        Preco  = _context.cardapioItems.First(pi => pi.Id == i.CardapioItemId).Preco
                        
                    }).ToList(),
                }).ToList();
            if (comanda == null)
            {
                return Results.NotFound("Comanda não encontrada.");
            }
            return Results.Ok(comanda);
        }

        // POST api/Comanda
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comandaCreate.CardapioItemIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio.");

            var novaComanda = new Comanda
            {
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };

            var comandaItens = new List<ComandaItem>();
            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
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

        // PUT api/Comanda/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            var comanda = _context.Comandas.FirstOrDefault(u => u.Id == id);
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comanda is null)
                return Results.NotFound($"Comanda {id} não encontrada!");

            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            comanda.NomeCliente = comandaUpdate.NomeCliente;

            foreach (var item in comandaUpdate.Itens)
            {
                if (item.Id > 0 && item.Remove == true)
                {
                    removerItemComanda(item.Id);
                }
                if (item.cardapíoItemId > 0)  // Corrigido: sem acento
                {
                    inserirItemComanda(comanda, item.cardapíoItemId);
                }
            }

            _context.SaveChanges();
            return Results.NoContent();
        }

        private void inserirItemComanda(Comanda comanda, int cardapioItemId)
        {
            _context.ComandaItens.Add(new ComandaItem
            {
                CardapioItemId = cardapioItemId,
                Comanda = comanda
            });
        }

        private void removerItemComanda(int id)
        {
            var comandaItem = _context.ComandaItens.FirstOrDefault(c => c.Id == id);
            if (comandaItem is not null)
            {
                _context.ComandaItens.Remove(comandaItem);
            }
        }

        // DELETE api/Comanda/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);  // Corrigido: usa _context, não a lista estática
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada");

            _context.Comandas.Remove(comanda);
            var comandaRemovida = _context.SaveChanges();
            if (comandaRemovida > 0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}