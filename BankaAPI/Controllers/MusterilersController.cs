using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankaAPI.Models;
using BankaAPI.Data;

namespace BankaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusterilerController : ControllerBase
    {
        private readonly Data.BankaDbContext _context;

        public MusterilerController(Data.BankaDbContext context) => _context = context;

        // GET: api/Musterilers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musteriler>>> GetMusteriler()
        {
            return await _context.Musteriler.ToListAsync();
        }

        // GET: api/Musterilers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Musteriler>> GetMusteriler(int id)
        {
            var musteriler = await _context.Musteriler.FindAsync(id);

            if (musteriler == null)
            {
                return NotFound();
            }

            return musteriler;
        }

        // PUT: api/Musterilers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusteriler(int id, Musteriler musteriler)
        {
            if (id != musteriler.MusteriNo)
            {
                return BadRequest();
            }

            _context.Entry(musteriler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusterilerExists(id))
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

        // POST: api/Musterilers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Musteriler>> PostMusteriler(Musteriler musteriler)
        {
            _context.Musteriler.Add(musteriler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusteriler", new { id = musteriler.MusteriNo }, musteriler);
        }

        // DELETE: api/Musterilers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusteriler(int id)
        {
            var musteriler = await _context.Musteriler.FindAsync(id);
            if (musteriler == null)
            {
                return NotFound();
            }

            _context.Musteriler.Remove(musteriler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusterilerExists(int id)
        {
            return _context.Musteriler.Any(e => e.MusteriNo == id);
        }
    }
}
