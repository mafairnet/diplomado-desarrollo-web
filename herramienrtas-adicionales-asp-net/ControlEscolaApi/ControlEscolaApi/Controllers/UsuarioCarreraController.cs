using ControlEscolaApi.Authorization;
using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCarreraController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public UsuarioCarreraController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/UsuarioCarrera
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCarrera>>> GetUsuarioCarreras([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.UsuarioCarrera == null)
            {
                return NotFound();
            }
            return await _dbContext.UsuarioCarrera.ToListAsync();
        }

        //GET: api/UsuarioCarrera/{id} [ID=(string)idEscuela_Carrera]
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCarrera>> GetUsuarioCarrera([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idCarrera = int.Parse(idArray[1]);

            if (_dbContext.UsuarioCarrera == null)
            {
                return NotFound();
            }
            var usuarioCarrera = await _dbContext.UsuarioCarrera.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCarrera == idCarrera);

            if (usuarioCarrera == null)
            {
                return NotFound();
            }

            return usuarioCarrera;
        }

        //POST: api/UsuarioCarrera
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<UsuarioCarrera>> PostUsuarioCarrera([FromHeader(Name = "X-API-Key")] string apiKey, UsuarioCarrera usuarioCarrera)
        {
            _dbContext.UsuarioCarrera.Add(usuarioCarrera);
            await _dbContext.SaveChangesAsync();
            var usuarioCarreraGuardado = await _dbContext.UsuarioCarrera.FirstOrDefaultAsync(e => e.IdUsuario == usuarioCarrera.IdUsuario && e.IdCarrera == usuarioCarrera.IdCarrera);
            return usuarioCarreraGuardado;
            //return CreatedAtAction(nameof(GetUsuarioCarrera), new { id = pais.ID, pais });
        }

        //DELETE: api/UsuarioCarrera/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCarrera([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idCarrera = int.Parse(idArray[1]);

            if (_dbContext.UsuarioCarrera == null)
            {
                return NotFound();
            }

            var usuarioCarrera = await _dbContext.UsuarioCarrera.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCarrera == idCarrera);
            if (usuarioCarrera == null)
            {
                return NotFound();
            }

            _dbContext.UsuarioCarrera.Remove(usuarioCarrera);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
