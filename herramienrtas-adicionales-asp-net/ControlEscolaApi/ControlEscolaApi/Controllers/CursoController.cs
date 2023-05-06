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
    public class CursoController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public CursoController(CatalogoEscolarContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get: api/Curso
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            if (_dbContext.Curso == null)
            {
                return NotFound();
            }
            return await _dbContext.Curso.ToListAsync();
        }

        //GET: api/Curso/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Curso == null)
            {
                return NotFound();
            }
            var curso = await _dbContext.Curso.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        //POST: api/Curso
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso([FromHeader(Name = "X-API-Key")] string apiKey, Curso curso)
        {
            _dbContext.Curso.Add(curso);
            await _dbContext.SaveChangesAsync();
            var cursoGuardado = await _dbContext.Curso.FindAsync(curso.ID);
            return cursoGuardado;
            //return CreatedAtAction(nameof(GetCurso), new { id = pais.ID, pais });
        }

        //PUT: app/Curso/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCurso([FromHeader(Name = "X-API-Key")] string apiKey, int id, Curso curso)
        {
            if (id != curso.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(curso).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        //DELETE: api/Curso/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Curso == null)
            {
                return NotFound();
            }

            var curso = await _dbContext.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _dbContext.Curso.Remove(curso);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(long id)
        {
            return (_dbContext.Curso?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
