namespace KC_SchoolManagement.Domain
{
    public class Admin : User
    {
        public int Id { get; set; }
        public new string FirstName { get; set; } = default!;
        public new string LastName { get; set; } = default!;
        public new required string Email { get; set; }
        public new required string Password { get; set; }
        public string Phone { get; set; } = default!;
        public DateTime DateCreated { get; set; }
    }
}
