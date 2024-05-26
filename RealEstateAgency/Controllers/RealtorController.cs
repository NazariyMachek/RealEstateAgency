using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Data;
using RealEstateAgency.Models;

namespace RealEstateAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealtorController : ControllerBase
    {
        private AppDataContext _context;

        public RealtorController(AppDataContext context)
        {
            _context = context;
        }

        #region CRUD
        [HttpGet("get")]
        public async Task<IEnumerable<Realtor>> Get()
        {
            return await _context.Realtors.ToListAsync();
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Realtor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _context.Realtors.FindAsync(id);

            return offer == null ? NotFound() : Ok(offer);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Realtor realtor)
        {
            await _context.Realtors.AddAsync(realtor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = realtor.id }, realtor);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Realtor realtor)
        {
            if (id != realtor.id)
            {
                return BadRequest();
            }

            _context.Entry(realtor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var realtorToDelete = await _context.Realtors.FindAsync(id);

            if (realtorToDelete == null)
            {
                return NotFound();
            }

            _context.Realtors.Remove(realtorToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
