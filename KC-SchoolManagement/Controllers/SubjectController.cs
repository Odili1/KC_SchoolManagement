using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
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

        //Delete
    }
}
