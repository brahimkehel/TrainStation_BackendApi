using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainStation_.Models;

namespace TrainStation_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class GaresController : ControllerBase
    {
        private readonly TrainStationContext _context;

        public GaresController(TrainStationContext context)
        {
            _context = context;
        }

        // GET: api/Gares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gare>>> GetGares()
        {
            return await _context.Gares.ToListAsync();
        }

        // GET: api/Gares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gare>> GetGare(int id)
        {
            var gare = await _context.Gares.FindAsync(id);

            if (gare == null)
            {
                return NotFound();
            }

            return gare;
        }

        // PUT: api/Gares/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGare(int id, Gare gare)
        {
            if (id != gare.Id)
            {
                return BadRequest();
            }

            _context.Entry(gare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GareExists(id))
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

        // POST: api/Gares
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gare>> PostGare(Gare gare)
        {
            _context.Gares.Add(gare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGare", new { id = gare.Id }, gare);
        }

        // DELETE: api/Gares/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gare>> DeleteGare(int id)
        {
            var gare = await _context.Gares.FindAsync(id);
            if (gare == null)
            {
                return NotFound();
            }

            _context.Gares.Remove(gare);
            await _context.SaveChangesAsync();

            return gare;
        }

        private bool GareExists(int id)
        {
            return _context.Gares.Any(e => e.Id == id);
        }
    }
}
