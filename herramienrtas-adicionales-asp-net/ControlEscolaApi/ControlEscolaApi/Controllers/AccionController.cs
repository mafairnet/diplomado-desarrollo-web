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
    public class AccionController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public AccionController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Accion
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accion>>> GetAccions([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Accion == null)
            {
                return NotFound();
            }
            return await _dbContext.Accion.ToListAsync();
        }

        //GET: api/Accion/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Accion>> GetAccion([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Accion == null)
            {
                return NotFound();
            }
            var accion = await _dbContext.Accion.FindAsync(id);

            if (accion == null)
            {
                return NotFound();
            }

            return accion;
        }

        //POST: api/Accion
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Accion>> PostAccion([FromHeader(Name = "X-API-Key")] string apiKey, Accion accion)
        {
            _dbContext.Accion.Add(accion);
            await _dbContext.SaveChangesAsync();
            var accionGuardado = await _dbContext.Accion.FindAsync(accion.ID);
            return accionGuardado;
            //return CreatedAtAction(nameof(GetAccion), new { id = pais.ID, pais });
        }

        //PUT: app/Accion/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAccion([FromHeader(Name = "X-API-Key")] string apiKey, int id, Accion accion)
        {
            if (id != accion.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(accion).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccionExists(id))
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

        //DELETE: api/Accion/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccion([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Accion == null)
            {
                return NotFound();
            }

            var accion = await _dbContext.Accion.FindAsync(id);
            if (accion == null)
            {
                return NotFound();
            }

            _dbContext.Accion.Remove(accion);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AccionExists(long id)
        {
            return (_dbContext.Accion?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
