using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVibe.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Student>? Students { get; set; } = new List<Student>();
        public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();
        public virtual ICollection<Instructor>? Instructors { get; set; } = new List<Instructor>();
    }
}
