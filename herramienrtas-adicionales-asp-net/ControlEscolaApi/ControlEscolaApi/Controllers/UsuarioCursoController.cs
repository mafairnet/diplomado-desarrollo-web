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
    public class UsuarioCursoController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public UsuarioCursoController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/UsuarioCurso
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCurso>>> GetUsuarioCursos([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.UsuarioCurso == null)
            {
                return NotFound();
            }
            return await _dbContext.UsuarioCurso.ToListAsync();
        }

        //GET: api/UsuarioCurso/{id} [ID=(string)idEscuela_Carrera]
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCurso>> GetUsuarioCurso([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int IdCurso = int.Parse(idArray[1]);

            if (_dbContext.UsuarioCurso == null)
            {
                return NotFound();
            }
            var usuarioCurso = await _dbContext.UsuarioCurso.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCurso == IdCurso);

            if (usuarioCurso == null)
            {
                return NotFound();
            }

            return usuarioCurso;
        }

        //POST: api/UsuarioCurso
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<UsuarioCurso>> PostUsuarioCurso([FromHeader(Name = "X-API-Key")] string apiKey, UsuarioCurso usuarioCurso)
        {
            _dbContext.UsuarioCurso.Add(usuarioCurso);
            await _dbContext.SaveChangesAsync();
            var usuarioCursoGuardado = await _dbContext.UsuarioCurso.FirstOrDefaultAsync(e => e.IdUsuario == usuarioCurso.IdUsuario && e.IdCurso == usuarioCurso.IdCurso);
            return usuarioCursoGuardado;
            //return CreatedAtAction(nameof(GetUsuarioCurso), new { id = pais.ID, pais });
        }

        //DELETE: api/UsuarioCurso/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCurso([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int IdCurso = int.Parse(idArray[1]);

            if (_dbContext.UsuarioCurso == null)
            {
                return NotFound();
            }

            var usuarioCurso = await _dbContext.UsuarioCurso.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCurso == IdCurso);
            if (usuarioCurso == null)
            {
                return NotFound();
            }

            _dbContext.UsuarioCurso.Remove(usuarioCurso);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
