using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC_SchoolManagement.Domain.Dtos
{
    public class RegisterUserDto
    {
        public required string Email { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public required string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
