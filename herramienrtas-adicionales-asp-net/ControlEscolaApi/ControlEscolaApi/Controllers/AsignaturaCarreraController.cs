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
    public class AsignaturaCarreraController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public AsignaturaCarreraController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/AsignaturaCarrera
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignaturaCarrera>>> GetAsignaturaCarreras([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.AsignaturaCarrera == null)
            {
                return NotFound();
            }
            return await _dbContext.AsignaturaCarrera.ToListAsync();
        }

        //GET: api/AsignaturaCarrera/{id} [ID=(string)idCarrera_IdAsignatura]
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<AsignaturaCarrera>> GetAsignaturaCarrera([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idCarrera = int.Parse(idArray[0]);
            int idAsignatura = int.Parse(idArray[1]);

            if (_dbContext.AsignaturaCarrera == null)
            {
                return NotFound();
            }
            var asignaturaCarrera = await _dbContext.AsignaturaCarrera.FirstOrDefaultAsync(e => e.IdAsignatura == idAsignatura && e.IdCarrera == idCarrera);

            if (asignaturaCarrera == null)
            {
                return NotFound();
            }

            return asignaturaCarrera;
        }

        //POST: api/AsignaturaCarrera
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<AsignaturaCarrera>> PostAsignaturaCarrera([FromHeader(Name = "X-API-Key")] string apiKey, AsignaturaCarrera asignaturaCarrera)
        {
            _dbContext.AsignaturaCarrera.Add(asignaturaCarrera);
            await _dbContext.SaveChangesAsync();
            var asignaturaCarreraGuardado = await _dbContext.AsignaturaCarrera.FirstOrDefaultAsync(e => e.IdAsignatura == asignaturaCarrera.IdAsignatura && e.IdCarrera == asignaturaCarrera.IdCarrera);
            return asignaturaCarreraGuardado;
            //return CreatedAtAction(nameof(GetAsignaturaCarrera), new { id = pais.ID, pais });
        }

        //DELETE: api/AsignaturaCarrera/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignaturaCarrera([FromHeader(Name = "X-API-Key")] string apiKey, string id)
        {
            String[] idArray = id.Split('_');

            int idCarrera = int.Parse(idArray[0]);
            int idAsignatura = int.Parse(idArray[1]);

            if (_dbContext.AsignaturaCarrera == null)
            {
                return NotFound();
            }

            var asignaturaCarrera = await _dbContext.AsignaturaCarrera.FirstOrDefaultAsync(e => e.IdAsignatura == idAsignatura && e.IdCarrera == idCarrera);
            if (asignaturaCarrera == null)
            {
                return NotFound();
            }

            _dbContext.AsignaturaCarrera.Remove(asignaturaCarrera);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
