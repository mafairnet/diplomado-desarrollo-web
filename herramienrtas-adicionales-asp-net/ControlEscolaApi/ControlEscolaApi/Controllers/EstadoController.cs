using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoContext _dbContext;
        public EstadoController(EstadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            if (_dbContext.Estado == null)
            {
                return NotFound();
            }
            return await _dbContext.Estado.ToListAsync();
        }

        //GET: api/Pais/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            if (_dbContext.Estado == null)
            {
                return NotFound();
            }
            var estado = await _dbContext.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        //POST: api/Pais
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            _dbContext.Estado.Add(estado);
            await _dbContext.SaveChangesAsync();
            var estadoGuardado = await _dbContext.Estado.FindAsync(estado.ID);
            return estadoGuardado;
            //return CreatedAtAction(nameof(GetPais), new { id = pais.ID, pais });
        }

        //PUT: app/Pais/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPais(int id, Estado estado)
        {
            if (id != estado.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(estado).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        //DELETE: api/Pais/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            if (_dbContext.Estado == null)
            {
                return NotFound();
            }

            var estado = await _dbContext.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _dbContext.Estado.Remove(estado);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(long id)
        {
            return (_dbContext.Estado?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
