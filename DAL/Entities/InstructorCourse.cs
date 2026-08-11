using EduVibe.Models.Entities;

namespace EduVibe.Models.Entities
{
    public class InstructorCourse
    {
        public int Id { get; set; }

        public string InstructorId { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
