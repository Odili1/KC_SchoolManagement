using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC_SchoolManagement.Domain.Dtos
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public UserType UserType { get; set; }
    }
}
