using System;

namespace EduVibe.Models.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string? ActionType { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}