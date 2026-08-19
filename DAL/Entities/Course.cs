using EduVibe.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduVibe.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? DurationInHours { get; set; }
        public decimal? Price { get; set; }
        public string CourseLevel { get; set; } = string.Empty;
        [NotMapped]
        public ICollection<Instructor>? Instructors { get; set; } = new List<Instructor>();
        public virtual int DepartmentId { get; set; } 
        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
