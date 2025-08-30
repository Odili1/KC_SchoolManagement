using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KC_SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private KC_SchoolManagementDbContext _context;
        public SubjectController(KC_SchoolManagementDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get All
        [HttpGet]
        public async Task<List<Subject>> GetAllClasses()
        {
            return await _context.Subjects.ToListAsync();
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // Update
        [HttpPost]
        [Authorize(Roles = "Teacher")]

        public async Task<ActionResult> UpdateSubject(int id, Subject updateSubject)
        {
            //Check if subject exists
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound(new {message=$"Subject with id {id} does not exist"});
            }

            subject.Title = updateSubject.Title;
            subject.Description = updateSubject.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound(new { message = $"Subject with id {id} does not exist" });
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
