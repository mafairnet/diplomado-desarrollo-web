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
    public class BitacoraController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public BitacoraController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Bitacora
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bitacora>>> GetBitacoras([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Bitacora == null)
            {
                return NotFound();
            }
            return await _dbContext.Bitacora.ToListAsync();
        }

        //GET: api/Bitacora/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Bitacora>> GetBitacora([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Bitacora == null)
            {
                return NotFound();
            }
            var bitacora = await _dbContext.Bitacora.FindAsync(id);

            if (bitacora == null)
            {
                return NotFound();
            }

            return bitacora;
        }

        //POST: api/Bitacora
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Bitacora>> PostBitacora([FromHeader(Name = "X-API-Key")] string apiKey, Bitacora bitacora)
        {
            _dbContext.Bitacora.Add(bitacora);
            await _dbContext.SaveChangesAsync();
            var bitacoraGuardado = await _dbContext.Bitacora.FindAsync(bitacora.ID);
            return bitacoraGuardado;
            //return CreatedAtAction(nameof(GetBitacora), new { id = pais.ID, pais });
        }

        //PUT: app/Bitacora/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBitacora([FromHeader(Name = "X-API-Key")] string apiKey, int id, Bitacora bitacora)
        {
            if (id != bitacora.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(bitacora).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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

        //DELETE: api/Bitacora/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBitacora([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Bitacora == null)
            {
                return NotFound();
            }

            var bitacora = await _dbContext.Bitacora.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            _dbContext.Bitacora.Remove(bitacora);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool BitacoraExists(long id)
        {
            return (_dbContext.Bitacora?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
