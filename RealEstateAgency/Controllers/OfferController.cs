using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Data;
using RealEstateAgency.Models;

namespace RealEstateAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private AppDataContext _context; 

        public OfferController(AppDataContext context)
        {
            _context = context;
        }

        #region CRUD
        [HttpGet("get")]
        public async Task<IEnumerable<Offer>> Get()
        {
            return await _context.Offers.ToListAsync();
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Offer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _context.Offers.FindAsync(id);

            return offer == null ? NotFound() : Ok(offer);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = offer.id }, offer);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Offer offer)
        {
            if (id != offer.id)
            {
                return BadRequest();
            }

            _context.Entry(offer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var offerToDelete = await _context.Offers.FindAsync(id);

            if (offerToDelete == null)
            {
                return NotFound();
            }

            _context.Offers.Remove(offerToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Query
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<Offer>> GetByUser(int id)
        {
            return await _context.Offers.FromSqlRaw($"select * from offers where \"userid\" = {id}").ToListAsync();
        }
        #endregion
    }
}
