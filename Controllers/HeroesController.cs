using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heroes.API.Data;
using Heroes.API.Model;

namespace Heroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroesContext _context;

        public HeroesController(HeroesContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroValue>>> GetHeroValue()
        {
            return await _context.HeroValue.ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroValue>> GetHeroValue(int id)
        {
            var heroValue = await _context.HeroValue.FindAsync(id);

            if (heroValue == null)
            {
                return NotFound();
            }

            return heroValue;
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroValue(int id, HeroValue heroValue)
        {
            if (id != heroValue.id)
            {
                return BadRequest();
            }

            _context.Entry(heroValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroValueExists(id))
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

        // POST: api/Heroes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HeroValue>> PostHeroValue(HeroValue heroValue)
        {
            _context.HeroValue.Add(heroValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeroValue", new { id = heroValue.id }, heroValue);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HeroValue>> DeleteHeroValue(int id)
        {
            var heroValue = await _context.HeroValue.FindAsync(id);
            if (heroValue == null)
            {
                return NotFound();
            }

            _context.HeroValue.Remove(heroValue);
            await _context.SaveChangesAsync();

            return heroValue;
        }

        private bool HeroValueExists(int id)
        {
            return _context.HeroValue.Any(e => e.id == id);
        }
    }
}
