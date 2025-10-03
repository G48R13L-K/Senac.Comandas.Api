using System.Runtime.InteropServices.Marshalling;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        static List<Usuario> usuarios = new List<Usuario>() {
        new Usuario
            {
                Id = 1,
                Nome = "adm",
                Email = "adm@adm.com",
                Senha = "123"

            },
        new Usuario
        {
            Id = 2,
            Nome = "usuario",
            Email = "usuario@usuario.com",
            Senha = "usuario"
        }
        };
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return Results.NotFound("Usuário não encontrado");
            }
            else
            {
                return Results.Ok(usuario);
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id }", usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
