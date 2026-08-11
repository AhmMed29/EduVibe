namespace EduVibe.Models.Entities
{
    public class Student 
    {
        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public StuAddress Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Gender { get; set; }
        public virtual int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
    public class StuAddress
    {
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
