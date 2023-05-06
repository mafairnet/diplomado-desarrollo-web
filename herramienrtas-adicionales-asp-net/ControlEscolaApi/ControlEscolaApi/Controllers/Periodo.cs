using ControlEscolaApi.Authorization;
using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public PeriodoController(CatalogoEscolarContext dbContext) {
            _dbContext = dbContext;
        }

        //Get: api/Periodoes
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos([FromHeader(Name = "X-API-Key")] string apiKey) {
            if (_dbContext.Periodo == null) {
                return NotFound();
            }
            return await _dbContext.Periodo.ToListAsync();
        }

        //GET: api/Periodo/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Periodo>> GetPeriodo([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Periodo == null)
            {
                return NotFound();
            }
            var periodo = await _dbContext.Periodo.FindAsync(id);

            if (periodo == null) {
                return NotFound();
            }

            return periodo;
        }

        //POST: api/Periodo
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Periodo>> PostPeriodo([FromHeader(Name = "X-API-Key")] string apiKey, Periodo periodo) {
            _dbContext.Periodo.Add(periodo);
            await _dbContext.SaveChangesAsync();
            var periodoGuardado = await _dbContext.Periodo.FindAsync(periodo.ID);
            return periodoGuardado;
            //return CreatedAtAction(nameof(GetPeriodo), new { id = periodo.ID, periodo });
        }

        //PUT: app/Periodo/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPeriodo([FromHeader(Name = "X-API-Key")] string apiKey, int id, Periodo periodo) {
            if (id != periodo.ID) {
                return BadRequest();
            }

            _dbContext.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PeriodoExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE: api/Periodo/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriodo([FromHeader(Name = "X-API-Key")] string apiKey, int id) {
            if (_dbContext.Periodo == null) {
                return NotFound();
            }

            var periodo = await _dbContext.Periodo.FindAsync(id);
            if (periodo == null) {
                return NotFound();
            }

            _dbContext.Periodo.Remove(periodo);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PeriodoExists(long id) {
            return (_dbContext.Periodo?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
