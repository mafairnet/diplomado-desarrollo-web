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
    public class CalificacionCursoUsuarioController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public CalificacionCursoUsuarioController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/CalificacionCursoUsuario
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalificacionCursoUsuario>>> GetCalificacionCursoUsuarios([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.CalificacionCursoUsuario == null)
            {
                return NotFound();
            }
            return await _dbContext.CalificacionCursoUsuario.ToListAsync();
        }

        //GET: api/CalificacionCursoUsuario/{id} [ID=(string)idEscuela_Carrera]
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<CalificacionCursoUsuario>> GetCalificacionCursoUsuario([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int IdCurso = int.Parse(idArray[1]);
            int IdCalificacion = int.Parse(idArray[2]);

            if (_dbContext.CalificacionCursoUsuario == null)
            {
                return NotFound();
            }
            var calificacionCursoUsuario = await _dbContext.CalificacionCursoUsuario.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCurso == IdCurso && e.IdCalificacion == IdCalificacion);

            if (calificacionCursoUsuario == null)
            {
                return NotFound();
            }

            return calificacionCursoUsuario;
        }

        //POST: api/CalificacionCursoUsuario
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<CalificacionCursoUsuario>> PostCalificacionCursoUsuario([FromHeader(Name = "X-API-Key")] string apiKey, CalificacionCursoUsuario calificacionCursoUsuario)
        {
            _dbContext.CalificacionCursoUsuario.Add(calificacionCursoUsuario);
            await _dbContext.SaveChangesAsync();
            var calificacionCursoUsuarioGuardado = await _dbContext.CalificacionCursoUsuario.FirstOrDefaultAsync(e => e.IdUsuario == calificacionCursoUsuario.IdUsuario && e.IdCurso == calificacionCursoUsuario.IdCurso);
            return calificacionCursoUsuarioGuardado;
            //return CreatedAtAction(nameof(GetCalificacionCursoUsuario), new { id = pais.ID, pais });
        }

        //DELETE: api/CalificacionCursoUsuario/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacionCursoUsuario([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int IdCurso = int.Parse(idArray[1]);
            int IdCalificacion = int.Parse(idArray[2]);

            if (_dbContext.CalificacionCursoUsuario == null)
            {
                return NotFound();
            }

            var calificacionCursoUsuario = await _dbContext.CalificacionCursoUsuario.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdCurso == IdCurso && e.IdCalificacion == IdCalificacion);
            if (calificacionCursoUsuario == null)
            {
                return NotFound();
            }

            _dbContext.CalificacionCursoUsuario.Remove(calificacionCursoUsuario);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
