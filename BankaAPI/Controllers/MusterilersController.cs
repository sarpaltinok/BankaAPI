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

        // GET: api/Musteriler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musteri>>> GetMusteriler()
        {
            return await _context.Musteriler.ToListAsync();
        }

        // GET: api/Musteriler/5
        [HttpGet("{musteriNo}")]
        public async Task<ActionResult<Musteri>> GetMusteri(int musteriNo)
        {
            var musteri = await _context.Musteriler.FindAsync(musteriNo);

            if (musteri == null)
            {
                return NotFound();
            }

            return musteri;
        }

        // PUT: api/Musteriler/5
        [HttpPut("{musteriNo}")]
        public async Task<IActionResult> PutMusteri(int musteriNo, Musteri musteri)
        {
            if (musteriNo != musteri.MusteriNo)
            {
                return BadRequest();
            }

            _context.Entry(musteri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusteriExists(musteriNo))
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Musteri>> PostMusteri(Musteri musteri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Aynı telefon numarasına sahip müşteri var mı kontrol et
            bool musteriVarMi = await _context.Musteriler
                .AnyAsync(m => m.Telefon == musteri.Telefon);

            if (musteriVarMi)
            {
                return BadRequest("Bu telefon numarasına sahip bir müşteri zaten kayıtlı.");
            }

            // Kayıt tarihi ata
            musteri.KayitTarihi = DateTime.Now;

            _context.Musteriler.Add(musteri);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMusteri), new { musteriNo = musteri.MusteriNo }, musteri);
        }



        // DELETE: api/Musteriler/5
        [HttpDelete("{musteriNo}")]
        public async Task<IActionResult> DeleteMusteri(int musteriNo)
        {
            var musteri = await _context.Musteriler.FindAsync(musteriNo);
            if (musteri == null)
            {
                return NotFound();
            }

            _context.Musteriler.Remove(musteri);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // helper method
        private bool MusteriExists(int musteriNo)
        {
            return _context.Musteriler.Any(e => e.MusteriNo == musteriNo);
        }
    }
}