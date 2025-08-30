namespace KC_SchoolManagement.Domain
{
    public class Teacher : User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int Age { get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
