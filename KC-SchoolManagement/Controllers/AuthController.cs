using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Domain.Dtos;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KC_SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly KC_SchoolManagementDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(KC_SchoolManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser(RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userType = userDto.UserType.ToString();
            bool userExists = false;

            if (userType == UserType.Student.ToString())
            {
                userExists = await _context.Students.AnyAsync(s => s.Email == userDto.Email);
            }
            else if (userType == UserType.Teacher.ToString())
            {
                userExists = await _context.Teachers.AnyAsync(t => t.Email == userDto.Email);

            }
            else if (userType == UserType.Admin.ToString())
            {
                userExists = await _context.Admins.AnyAsync(t => t.Email == userDto.Email);
            }

            if (userExists)
            {
                return BadRequest("Email is already in use.");
            }

            var newUser = CreateUser(userType, userDto);

            _context.Set<User>().Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new {Message = "User Registered successfully."});
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Password) || string.IsNullOrEmpty(loginDto.Email))
            {
                return BadRequest("Email and password are required");
            }

            var user = await RetriveUserAsync(loginDto.Email, loginDto.LoginAs.ToString());

            if (user == null)
            {
                return Unauthorized("Invalid Email or password");
            }

            if (!VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid Email or password");
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseDto
            {
                Email = user.Email,
                UserType = user.UserType,
                Token = token
            });
        }

        // ----- PRIVATE METHODS -----

        private string GenerateJwtToken(User user)
        {
            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            // Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));

            // Credential
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Assign to Token
            var token = new JwtSecurityToken(
              issuer: _configuration["Issuet"],
              audience: _configuration["Audience"],
              claims: claims,
              signingCredentials: creds,
              expires: DateTime.Now.AddHours(1)
             );

            // Create token string
            var newToken = new JwtSecurityTokenHandler() .WriteToken(token);

            return newToken;
        }

        private bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedPassword))
            {
                return false;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, storedPassword);

            return isValidPassword;
        }

        private async Task<User> RetriveUserAsync(string email, string userType)
        {
            return userType switch
            {
                "Admin" => await _context.Admins.FirstOrDefaultAsync(em => em.Email == email),
                "Teacher" => await _context.Teachers.FirstOrDefaultAsync(em => em.Email == email),
                "Student" => await _context.Students.FirstOrDefaultAsync(em => em.Email == email),

                _ => throw new ArgumentException("Invalid user type")
            };
        }

        private static User CreateUser(string userType, RegisterUserDto userDto)
        {
            return userType switch
            {
                "Admin" => new Admin
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    UserType = userDto.UserType,
                },
                "Teacher" => new Teacher
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    UserType = userDto.UserType,
                },
                "Student" => new Student
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    UserType = userDto.UserType,
                },

                _ => throw new ArgumentException("Invalid user type")
            };
        }
    }
}
