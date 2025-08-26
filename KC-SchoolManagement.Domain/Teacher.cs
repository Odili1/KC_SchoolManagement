using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC_SchoolManagement.Domain
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public required string Email { get; set; }
        public int Age {  get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
