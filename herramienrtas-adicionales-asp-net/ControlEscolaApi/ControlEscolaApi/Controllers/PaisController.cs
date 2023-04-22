using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly PaisContext _dbContext;
        public PaisController(PaisContext dbContext) {
            _dbContext = dbContext;
        }

        //Get: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises() {
            if (_dbContext.Pais == null) {
                return NotFound();
            }
            return await _dbContext.Pais.ToListAsync();
        }

        //GET: api/Pais/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            if (_dbContext.Pais == null)
            {
                return NotFound();
            }
            var pais = await _dbContext.Pais.FindAsync(id);

            if (pais == null) {
                return NotFound();
            }

            return pais;
        }

        //POST: api/Pais
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais) {
            _dbContext.Pais.Add(pais);
            await _dbContext.SaveChangesAsync();
            var paisGuardado = await _dbContext.Pais.FindAsync(pais.ID);
            return paisGuardado;
            //return CreatedAtAction(nameof(GetPais), new { id = pais.ID, pais });
        }

        //PUT: app/Pais/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPais(int id, Pais pais) {
            if (id != pais.ID) {
                return BadRequest();
            }

            _dbContext.Entry(pais).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PaisExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE: api/Pais/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais(int id) {
            if (_dbContext.Pais == null) {
                return NotFound();
            }

            var pais = await _dbContext.Pais.FindAsync(id);
            if (pais == null) {
                return NotFound();
            }

            _dbContext.Pais.Remove(pais);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PaisExists(long id) {
            return (_dbContext.Pais?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
