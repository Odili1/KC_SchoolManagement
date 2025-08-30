namespace KC_SchoolManagement.Domain
{
    public class Student : User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public required string Email { get; set; }
        public required string Password { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Class? Class { get; set; }
        public DateTime? EnrollmentYear { get; set; }
        public Subject? Subject { get; set; }
    }
}
