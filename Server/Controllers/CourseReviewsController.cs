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
    public class CourseReviewsController : ControllerBase
    {
        private readonly codesomeServerContext _context;

        public CourseReviewsController(codesomeServerContext context)
        {
            _context = context;
        }

        // GET: api/CourseReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReview>>> GetCourseReview()
        {
          if (_context.CourseReview == null)
          {
              return NotFound();
          }
            return await _context.CourseReview.ToListAsync();
        }

        // GET: api/CourseReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReview>> GetCourseReview(int id)
        {
          if (_context.CourseReview == null)
          {
              return NotFound();
          }
            var courseReview = await _context.CourseReview.FindAsync(id);

            if (courseReview == null)
            {
                return NotFound();
            }

            return courseReview;
        }

        // PUT: api/CourseReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseReview(int id, CourseReview courseReview)
        {
            if (id != courseReview.Id)
            {
                return BadRequest();
            }

            _context.Entry(courseReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseReviewExists(id))
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

        // POST: api/CourseReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseReview>> PostCourseReview(CourseReview courseReview)
        {
          if (_context.CourseReview == null)
          {
              return Problem("Entity set 'codesomeServerContext.CourseReview'  is null.");
          }
            _context.CourseReview.Add(courseReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseReview", new { id = courseReview.Id }, courseReview);
        }

        // DELETE: api/CourseReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseReview(int id)
        {
            if (_context.CourseReview == null)
            {
                return NotFound();
            }
            var courseReview = await _context.CourseReview.FindAsync(id);
            if (courseReview == null)
            {
                return NotFound();
            }

            _context.CourseReview.Remove(courseReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseReviewExists(int id)
        {
            return (_context.CourseReview?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
