namespace KC_SchoolManagement.Domain
{
    public class Subject
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = default!;
        public List<Class>? Classes { get; set; }
    }
}
