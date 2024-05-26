using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Data;
using RealEstateAgency.Models;

namespace RealEstateAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private AppDataContext _context;

        public ReviewController(AppDataContext context)
        {
            _context = context;
        }

        #region CRUD
        [HttpGet("get")]
        public async Task<IEnumerable<Review>> Get()
        {
            return await _context.Reviews.ToListAsync();
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _context.Reviews.FindAsync(id);

            return offer == null ? NotFound() : Ok(offer);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = review.id }, review);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Review review)
        {
            if (id != review.id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewToDelete = await _context.Reviews.FindAsync(id);

            if (reviewToDelete == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(reviewToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Query
        [HttpGet("offer/{id}")]
        public async Task<IEnumerable<Review>> GetByOfferId(int id)
        {
            return await _context.Reviews.FromSqlRaw($"select * from reviews where \"offerid\" = {id}").ToListAsync();
        }
        #endregion
    }
}
