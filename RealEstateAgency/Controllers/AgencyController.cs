using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Data;
using RealEstateAgency.Models;

namespace RealEstateAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private AppDataContext _context;

        public AgencyController(AppDataContext context)
        {
            _context = context;
        }

        #region CRUD
        [HttpGet("get")]
        public async Task<IEnumerable<Agency>> Get()
        {
            return await _context.Agencies.ToListAsync();
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Agency), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _context.Agencies.FindAsync(id);

            return offer == null ? NotFound() : Ok(offer);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Agency agency)
        {
            await _context.Agencies.AddAsync(agency);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = agency.id }, agency);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Agency agency)
        {
            if (id != agency.id)
            {
                return BadRequest();
            }

            _context.Entry(agency).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var agencyToDelete = await _context.Agencies.FindAsync(id);

            if (agencyToDelete == null)
            {
                return NotFound();
            }

            _context.Agencies.Remove(agencyToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
