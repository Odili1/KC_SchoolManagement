using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KC_SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private KC_SchoolManagementDbContext _context;
        public StudentController(KC_SchoolManagementDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult> CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get All
        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var user = await _context.Students.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // Update
        //[HttpPut]
        //public async 

        //Delete
    }
}
