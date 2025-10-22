using System.Runtime.InteropServices.Marshalling;
using Comandas.Api.DTOs;
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
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if (usuarioCreate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter no minimo 6 caracteres.");
            if (usuarioCreate.Nome.Length < 3) 
                return Results.BadRequest("O nome deve ter no minimo 3 caracteres.");
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
                return Results.BadRequest("O email deve ser valido.");

           var usuario = new Usuario
           {
               Id = usuarios.Count + 1,
               Nome = usuarioCreate.Nome,
               Email = usuarioCreate.Email,
               Senha= usuarioCreate.Senha,
           };
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}",usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            //retorna mensagem se não encontrar 
            if (usuario is null) 
                return Results.NotFound($"Usuario do id {id} não encontrado.");

            //atualiza usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            return Results.NoContent();
                
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(c => c.Id == id);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrada");
            var usuarioRemovido = usuarios.Remove(usuario);
            if (usuarioRemovido)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
    }

