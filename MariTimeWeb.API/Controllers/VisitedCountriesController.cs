using Microsoft.AspNetCore.Mvc;
using MariTimeWeb.API.Data;
using MariTimeWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MariTimeWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitedCountryController : ControllerBase
    {
        private readonly MariTimeDbContext _context;

        public VisitedCountryController(MariTimeDbContext context)
        {
            _context = context;
        }

        // GET: api/VisitedCountry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitedCountry>>> GetVisitedCountries()
        {
            return await _context.VisitedCountries.Include(vc => vc.Ship).ToListAsync();
        }

        // GET: api/VisitedCountry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitedCountry>> GetVisitedCountry(int id)
        {
            var visitedCountry = await _context.VisitedCountries.Include(vc => vc.Ship)
                                                                .FirstOrDefaultAsync(vc => vc.VisitedCountryId == id);

            if (visitedCountry == null)
            {
                return NotFound();
            }

            return visitedCountry;
        }

        // PUT: api/VisitedCountry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitedCountry(int id, VisitedCountry visitedCountry)
        {
            if (id != visitedCountry.VisitedCountryId)
            {
                return BadRequest();
            }

            _context.Entry(visitedCountry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitedCountryExists(id))
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

        // POST: api/VisitedCountry
        [HttpPost]
        public async Task<ActionResult<VisitedCountry>> PostVisitedCountry(VisitedCountry visitedCountry)
        {
            _context.VisitedCountries.Add(visitedCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVisitedCountry), new { id = visitedCountry.VisitedCountryId }, visitedCountry);
        }

        // DELETE: api/VisitedCountry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitedCountry(int id)
        {
            var visitedCountry = await _context.VisitedCountries.FindAsync(id);
            if (visitedCountry == null)
            {
                return NotFound();
            }

            _context.VisitedCountries.Remove(visitedCountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitedCountryExists(int id)
        {
            return _context.VisitedCountries.Any(e => e.VisitedCountryId == id);
        }
    }
}
