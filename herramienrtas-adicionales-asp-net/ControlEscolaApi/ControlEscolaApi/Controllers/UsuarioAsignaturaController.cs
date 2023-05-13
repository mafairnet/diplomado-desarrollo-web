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
    public class UsuarioAsignaturaController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public UsuarioAsignaturaController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/UsuarioAsignatura
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioAsignatura>>> GetUsuarioAsignaturas([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.UsuarioAsignatura == null)
            {
                return NotFound();
            }
            return await _dbContext.UsuarioAsignatura.ToListAsync();
        }

        //GET: api/UsuarioAsignatura/{id} [ID=(string)idEscuela_Carrera]
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioAsignatura>> GetUsuarioAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idAsignatura = int.Parse(idArray[1]);

            if (_dbContext.UsuarioAsignatura == null)
            {
                return NotFound();
            }
            var usuarioAsignatura = await _dbContext.UsuarioAsignatura.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdAsignatura == idAsignatura);

            if (usuarioAsignatura == null)
            {
                return NotFound();
            }

            return usuarioAsignatura;
        }

        //POST: api/UsuarioAsignatura
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<UsuarioAsignatura>> PostUsuarioAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, UsuarioAsignatura usuarioAsignatura)
        {
            _dbContext.UsuarioAsignatura.Add(usuarioAsignatura);
            await _dbContext.SaveChangesAsync();
            var usuarioAsignaturaGuardado = await _dbContext.UsuarioAsignatura.FirstOrDefaultAsync(e => e.IdUsuario == usuarioAsignatura.IdUsuario && e.IdAsignatura == usuarioAsignatura.IdAsignatura);
            return usuarioAsignaturaGuardado;
            //return CreatedAtAction(nameof(GetUsuarioAsignatura), new { id = pais.ID, pais });
        }

        //DELETE: api/UsuarioAsignatura/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idAsignatura = int.Parse(idArray[1]);

            if (_dbContext.UsuarioAsignatura == null)
            {
                return NotFound();
            }

            var usuarioAsignatura = await _dbContext.UsuarioAsignatura.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdAsignatura == idAsignatura);
            if (usuarioAsignatura == null)
            {
                return NotFound();
            }

            _dbContext.UsuarioAsignatura.Remove(usuarioAsignatura);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
