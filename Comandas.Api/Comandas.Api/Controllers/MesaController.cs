using Microsoft.AspNetCore.Mvc;
using Comandas.Api.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        public List<Mesa> mesas = new List<Mesa> {
            new Mesa
            {       Id = 1,
                    NumeroMesa = 1,
                    SituacaoMesa = (int)SituacaoMesa.Livre,
            }, 
            new Mesa
            {       
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = (int)SituacaoMesa.Ocupada,
                }
        };
// GET: api/<MesaController>
[HttpGet]
        public IResult GetMesa()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
