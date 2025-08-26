using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC_SchoolManagement.Domain
{
    public class Class
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SubjectCode { get; set; }
        public List<Subject>? Subjects { get; set; }
        public Teacher? FormTeacher { get; set; }
        public List<Student>? Students { get; set; }
    }
}
