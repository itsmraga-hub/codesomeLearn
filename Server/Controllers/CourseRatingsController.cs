using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using codesome.Server.Data;
using codesome.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace codesome.Server.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class CourseRatingsController : ControllerBase
    {
        private readonly codesomeServerContext _context;

        public CourseRatingsController(codesomeServerContext context)
        {
            _context = context;
        }

        // GET: api/CourseRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseRating>>> GetCourseRating()
        {
          if (_context.CourseRating == null)
          {
              return NotFound();
          }
            return await _context.CourseRating.ToListAsync();
        }

        // GET: api/CourseRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseRating>> GetCourseRating(int id)
        {
          if (_context.CourseRating == null)
          {
              return NotFound();
          }
            var courseRating = await _context.CourseRating.FindAsync(id);

            if (courseRating == null)
            {
                return NotFound();
            }

            return courseRating;
        }

        // PUT: api/CourseRatings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseRating(int id, CourseRating courseRating)
        {
            if (id != courseRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(courseRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseRatingExists(id))
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

        // POST: api/CourseRatings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseRating>> PostCourseRating(CourseRating courseRating)
        {
          if (_context.CourseRating == null)
          {
              return Problem("Entity set 'codesomeServerContext.CourseRating'  is null.");
          }
            _context.CourseRating.Add(courseRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseRating", new { id = courseRating.Id }, courseRating);
        }

        // DELETE: api/CourseRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseRating(int id)
        {
            if (_context.CourseRating == null)
            {
                return NotFound();
            }
            var courseRating = await _context.CourseRating.FindAsync(id);
            if (courseRating == null)
            {
                return NotFound();
            }

            _context.CourseRating.Remove(courseRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseRatingExists(int id)
        {
            return (_context.CourseRating?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
