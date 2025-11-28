using System.Runtime.InteropServices.Marshalling;
using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {   //variavel que representa o banco de dados
        public ComandaDbContext _context { get; set; }
        //construtor que recebe o contexto do banco de dados
        public UsuarioController(ComandaDbContext context)
        {
            _context = context;
        }

    
        // GET: api/<UsuarioController>
        [HttpGet]
    public IResult Get()
        {
        var usuarios = _context.Usuarios.ToList();
        return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {   //busca usuario pelo id
            
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);
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
            var emailExistente = _context.Usuarios
                .FirstOrDefault(u => u.Email == usuarioCreate.Email);
            if(emailExistente is not null)
                return Results.BadRequest("O email já está em uso.");

            var usuario = new Usuario
           {
               Nome = usuarioCreate.Nome,
               Email = usuarioCreate.Email,
               Senha= usuarioCreate.Senha,
           };
            //adiciona usuario ao banco de dados
            _context.Usuarios.Add(usuario);
            //salva as alterações no banco de dados
            _context.SaveChanges();
            //retorna o usuario criado com o status 201
            return Results.Created($"/api/usuario/{usuario.Id}",usuario);

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            //busca usuario pelo id
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            //retorna mensagem se não encontrar 
            if (usuario is null) 
                return Results.NotFound($"Usuario do id {id} não encontrado.");

            //atualiza usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            //salva as alterações no banco de dados
            _context.SaveChanges();
            return Results.NoContent();
                
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {   //busca usuario pelo id
            var usuario = _context.Usuarios.FirstOrDefault(c => c.Id == id);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrada");

            _context.Usuarios.Remove(usuario);
            var usuarioRemovido = _context.SaveChanges();
            if (usuarioRemovido > 0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }

        //criar metodo de login aqui
        //post api/usuario/login
        [HttpPost("login")]
        public IResult Login([FromBody] LoginRequest loginRequest)
        {
            var usuario = _context.Usuarios.FirstOrDefault(
                u => u.Email == loginRequest.Email 
                && u.Senha == loginRequest.Senha);
            
            if (usuario is null)
            {
                return Results.Unauthorized();
            }
           
                return Results.Ok("Usuário autenticado");
        }
    }
    }

