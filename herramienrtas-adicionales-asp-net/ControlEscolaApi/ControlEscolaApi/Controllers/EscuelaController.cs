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
    public class EscuelaController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public EscuelaController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Escuela
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Escuela>>> GetEscuelas([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Escuela == null)
            {
                return NotFound();
            }
            return await _dbContext.Escuela.ToListAsync();
        }

        //GET: api/Escuela/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Escuela>> GetEscuela([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Escuela == null)
            {
                return NotFound();
            }
            var escuela = await _dbContext.Escuela.FindAsync(id);

            if (escuela == null)
            {
                return NotFound();
            }

            return escuela;
        }

        //POST: api/Escuela
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Escuela>> PostEscuela([FromHeader(Name = "X-API-Key")] string apiKey, Escuela escuela)
        {
            _dbContext.Escuela.Add(escuela);
            await _dbContext.SaveChangesAsync();
            var escuelaGuardado = await _dbContext.Escuela.FindAsync(escuela.ID);
            return escuelaGuardado;
            //return CreatedAtAction(nameof(GetEscuela), new { id = pais.ID, pais });
        }

        //PUT: app/Escuela/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEscuela([FromHeader(Name = "X-API-Key")] string apiKey, int id, Escuela escuela)
        {
            if (id != escuela.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(escuela).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscuelaExists(id))
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

        //DELETE: api/Escuela/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscuela([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Escuela == null)
            {
                return NotFound();
            }

            var escuela = await _dbContext.Escuela.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }

            _dbContext.Escuela.Remove(escuela);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool EscuelaExists(long id)
        {
            return (_dbContext.Escuela?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
