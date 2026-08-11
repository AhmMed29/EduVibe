using EduVibe.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduVibe.Models.Entities
{
    public class CourseSchedule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        [Required]
        public string DayOfWeek { get; set; } = null!;

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string Room { get; set; } = null!;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
