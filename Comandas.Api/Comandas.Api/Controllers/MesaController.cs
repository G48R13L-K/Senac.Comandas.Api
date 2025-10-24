using Microsoft.AspNetCore.Mvc;
using Comandas.Api.Models;
using Comandas.Api.DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        public ComandaDbContext _context { get; set; }
        public MesaController(ComandaDbContext context)
        {
            _context = context;
        }
        // GET: api/<MesaController>
        [HttpGet]
        public IResult GetMesa()
        {
            var mesa = _context.Mesas.ToList();
            return Results.Ok(mesa);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return Results.NotFound("Mesa não encontrada");
            }
            else
            {
                return Results.Ok(mesa);
            }
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesaCreate)
        {
            if (mesaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");

            var novaMesa = new Mesa
            {
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = (int)SituacaoMesa.Livre
            };
            _context.Mesas.Add(novaMesa);
            _context.SaveChanges();
            return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequests mesaUpdate)
        {
            if (mesaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if(mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Livre)," + "1 (Ocupada) ou 2 (Reservada).");

            var mesa =_context.Mesas.FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada!");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(c => c.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa não encontrada");
             _context.Mesas.Remove(mesa);
            var mesaRemovida = _context.SaveChanges();
            if (mesaRemovida >0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
    }

