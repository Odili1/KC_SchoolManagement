using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]

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
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> UpdateStudent(int id, Student updateStudent)
        {
            //Check if student exists
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { message = $"Student with id {id} does not exist" });
            }

            student.FirstName = updateStudent.FirstName;
            student.LastName = updateStudent.LastName;
            student.DateOfBirth = updateStudent.DateOfBirth;
            student.EnrollmentYear = updateStudent.EnrollmentYear;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { message = $"Student with id {id} does not exist" });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
