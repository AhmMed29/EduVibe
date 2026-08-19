// this entity connecting the course table with the student table 
// enrollments : The Courses That The student paid for.
namespace EduVibe.Models.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
    
