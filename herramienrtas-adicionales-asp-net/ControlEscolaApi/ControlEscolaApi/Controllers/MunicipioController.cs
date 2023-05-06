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
    public class MunicipioController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public MunicipioController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Municipio
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Municipio>>> GetMunicipios([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Municipio == null)
            {
                return NotFound();
            }
            return await _dbContext.Municipio.ToListAsync();
        }

        //GET: api/Municipio/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio>> GetMunicipio([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Municipio == null)
            {
                return NotFound();
            }
            var municipio = await _dbContext.Municipio.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }

            return municipio;
        }

        //POST: api/Municipio
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Municipio>> PostMunicipio([FromHeader(Name = "X-API-Key")] string apiKey, Municipio municipio)
        {
            _dbContext.Municipio.Add(municipio);
            await _dbContext.SaveChangesAsync();
            var municipioGuardado = await _dbContext.Municipio.FindAsync(municipio.ID);
            return municipioGuardado;
            //return CreatedAtAction(nameof(GetMunicipio), new { id = pais.ID, pais });
        }

        //PUT: app/Municipio/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMunicipio([FromHeader(Name = "X-API-Key")] string apiKey, int id, Municipio municipio)
        {
            if (id != municipio.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(municipio).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExists(id))
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

        //DELETE: api/Municipio/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipio([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Municipio == null)
            {
                return NotFound();
            }

            var municipio = await _dbContext.Municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _dbContext.Municipio.Remove(municipio);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MunicipioExists(long id)
        {
            return (_dbContext.Municipio?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
