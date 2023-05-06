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
    public class AsignaturaController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public AsignaturaController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Asignatura
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetAsignaturas([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Asignatura == null)
            {
                return NotFound();
            }
            return await _dbContext.Asignatura.ToListAsync();
        }

        //GET: api/Asignatura/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Asignatura == null)
            {
                return NotFound();
            }
            var asignatura = await _dbContext.Asignatura.FindAsync(id);

            if (asignatura == null)
            {
                return NotFound();
            }

            return asignatura;
        }

        //POST: api/Asignatura
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Asignatura>> PostAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, Asignatura asignatura)
        {
            _dbContext.Asignatura.Add(asignatura);
            await _dbContext.SaveChangesAsync();
            var asignaturaGuardado = await _dbContext.Asignatura.FindAsync(asignatura.ID);
            return asignaturaGuardado;
            //return CreatedAtAction(nameof(GetAsignatura), new { id = pais.ID, pais });
        }

        //PUT: app/Asignatura/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, int id, Asignatura asignatura)
        {
            if (id != asignatura.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(asignatura).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE: api/Asignatura/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Asignatura == null)
            {
                return NotFound();
            }

            var asignatura = await _dbContext.Asignatura.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            _dbContext.Asignatura.Remove(asignatura);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignaturaExists(long id)
        {
            return (_dbContext.Asignatura?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
