using System.ComponentModel.DataAnnotations.Schema;
namespace EduVibe.Models.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public InsAddress Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? Salary { get; set; }
        [NotMapped]
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
    public class InsAddress
    {
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
