using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVibe.Models.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public decimal Grades { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
    
