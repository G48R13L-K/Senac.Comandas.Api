using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api;
using Comandas.Api.Models;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ComandaDbContext _context;

        public ReservasController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.id)
            {
                return BadRequest();
            }
            //atualização
            _context.Entry(reserva).State = EntityState.Modified;

            
            //mudar a situação para reservar 
            var novaMesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (novaMesa is null)
                return BadRequest("Mesa não encontrada.");
            novaMesa.SituacaoMesa = (int)SituacaoMesa.Reservada;

            //consulta dados da reserva original
            var reservaOriginal = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.id == id);
            //consulta dados da mesa original
            var numeroMesaOriginal = reservaOriginal.NumeroMesa;
            //consulta mesa original
            var mesaOriginal = await _context.Mesas.
                FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesaOriginal);
            mesaOriginal!.SituacaoMesa = (int)SituacaoMesa.Livre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            
            _context.Reservas.Add(reserva);
            //atualizar status da mesa

            //consultando a mesa prlo numero
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            //se não houver mesa, retornar erro
            if (mesa == null)
                {
                    return BadRequest("Mesa não encontrada.");
                }
            //se houver a mesa, verificar se está livre
            if (mesa is not null)
                {
                    //se a mesa não estiver livre, retorna erro
                    if(mesa.SituacaoMesa != (int)SituacaoMesa.Livre)
                    {
                        return BadRequest("Mesa não está disponível para reserva.");
                    }
                    //atualizando status da mesa para reservada
                     mesa.SituacaoMesa = (int)SituacaoMesa.Reservada;
                }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.id }, reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            //consulta a reserva
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            //consulta a mesa
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            {
                if(mesa == null)
                {
                    return BadRequest("Mesa não encontrada.");
                }
                //atualizando status da mesa para livre
                mesa.SituacaoMesa = (int)SituacaoMesa.Livre;
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.id == id);
        }
    }
}
