using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC_SchoolManagement.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Class? Class { get; set; }
        public DateTime? EnrollmentYear { get; set; }
        public Subject? Subject { get; set; }
    }
}
