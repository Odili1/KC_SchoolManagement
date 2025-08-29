using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KC_SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private KC_SchoolManagementDbContext _context;
        public TeacherController(KC_SchoolManagementDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult> CreateTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get All
        [HttpGet]
        public async Task<List<Teacher>> GetAllClasses()
        {
            return await _context.Teachers.ToListAsync();
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetById(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }


        // Update

        //Delete
    }
}
