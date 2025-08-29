using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KC_SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private KC_SchoolManagementDbContext _context;
        public ClassController(KC_SchoolManagementDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult> CreateClass(Class classParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Class.AddAsync(classParam);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get All
        [HttpGet]
        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Class.ToListAsync();
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetById(int id)
        {
            var classExist = await _context.Class.FindAsync(id);

            if (classExist == null)
            {
                return NotFound();
            }

            return Ok(classExist);
        }


        // Update 


        //Delete
    }
}
