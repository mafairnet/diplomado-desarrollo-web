using ControlEscolaApi.Authorization;
using ControlEscolaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly CatalogoEscolarContext _dbContext;
        public StatusController(CatalogoEscolarContext dbContext) {
            _dbContext = dbContext;
        }

        //Get: api/Status
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetAllStatus([FromHeader(Name = "X-API-Key")] string apiKey) {
            if (_dbContext.Status == null) {
                return NotFound();
            }
            return await _dbContext.Status.ToListAsync();
        }

        //GET: api/Status/{id}
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus([FromHeader(Name = "X-API-Key")] string apiKey, int id)
        {
            if (_dbContext.Status == null)
            {
                return NotFound();
            }
            var status = await _dbContext.Status.FindAsync(id);

            if (status == null) {
                return NotFound();
            }

            return status;
        }

        //POST: api/Status
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus([FromHeader(Name = "X-API-Key")] string apiKey, Status status) {
            _dbContext.Status.Add(status);
            await _dbContext.SaveChangesAsync();
            var statusGuardado = await _dbContext.Status.FindAsync(status.ID);
            return statusGuardado;
            //return CreatedAtAction(nameof(GetStatus), new { id = status.ID, status });
        }

        //PUT: app/Status/{id}
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStatus([FromHeader(Name = "X-API-Key")] string apiKey, int id, Status status) {
            if (id != status.ID) {
                return BadRequest();
            }

            _dbContext.Entry(status).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        //DELETE: api/Status/{id}
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus([FromHeader(Name = "X-API-Key")] string apiKey, int id) {
            if (_dbContext.Status == null) {
                return NotFound();
            }

            var status = await _dbContext.Status.FindAsync(id);
            if (status == null) {
                return NotFound();
            }

            _dbContext.Status.Remove(status);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(long id) {
            return (_dbContext.Status?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
