using ControlEscolaApi.Authorization;
using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public UsuarioController(CatalogoEscolarContext dbContext) {
            _dbContext = dbContext;
        }

        //Get: api/Usuarios
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios([FromHeader(Name="X-API-Key")] string apiKey) {
            if (_dbContext.Usuario == null) {
                return NotFound();
            }
            return await _dbContext.Usuario.ToListAsync();
        }

        //GET: api/Usuario/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Usuario == null)
            {
                return NotFound();
            }
            var pais = await _dbContext.Usuario.FindAsync(id);

            if (pais == null) {
                return NotFound();
            }

            return pais;
        }

        //POST: api/Pais
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromHeader(Name = "X-API-Key")] string apiKey,Usuario usuario) {
            _dbContext.Usuario.Add(usuario);
            await _dbContext.SaveChangesAsync();
            var usuarioGuardado = await _dbContext.Usuario.FindAsync(usuario.ID);
            return usuarioGuardado;
        }

        //PUT: app/Usuario/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuario([FromHeader(Name = "X-API-Key")] string apiKey,int id, Usuario usuario) {
            if (id != usuario.ID) {
                return BadRequest();
            }

            _dbContext.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE: api/Usuario/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromHeader(Name = "X-API-Key")] string apiKey,int id) {
            if (_dbContext.Usuario == null) {
                return NotFound();
            }

            var usuario = await _dbContext.Usuario.FindAsync(id);
            if (usuario == null) {
                return NotFound();
            }

            _dbContext.Usuario.Remove(usuario);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id) {
            return (_dbContext.Usuario?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
