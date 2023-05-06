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
    public class LocalidadController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public LocalidadController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Localidad
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localidad>>> GetLocalidades([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Localidad == null)
            {
                return NotFound();
            }
            return await _dbContext.Localidad.ToListAsync();
        }

        //GET: api/Localidad/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Localidad>> GetLocalidad([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Localidad == null)
            {
                return NotFound();
            }
            var localidad = await _dbContext.Localidad.FindAsync(id);

            if (localidad == null)
            {
                return NotFound();
            }

            return localidad;
        }

        //POST: api/Localidad
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Localidad>> PostLocalidad([FromHeader(Name = "X-API-Key")] string apiKey, Localidad localidad)
        {
            _dbContext.Localidad.Add(localidad);
            await _dbContext.SaveChangesAsync();
            var localidadGuardado = await _dbContext.Localidad.FindAsync(localidad.ID);
            return localidadGuardado;
            //return CreatedAtAction(nameof(GetLocalidad), new { id = pais.ID, pais });
        }

        //PUT: app/Localidad/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLocalidad([FromHeader(Name = "X-API-Key")] string apiKey, int id, Localidad localidad)
        {
            if (id != localidad.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(localidad).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalidadExists(id))
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

        //DELETE: api/Localidad/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalidad([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Localidad == null)
            {
                return NotFound();
            }

            var localidad = await _dbContext.Localidad.FindAsync(id);
            if (localidad == null)
            {
                return NotFound();
            }

            _dbContext.Localidad.Remove(localidad);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalidadExists(long id)
        {
            return (_dbContext.Localidad?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
