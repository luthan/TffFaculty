namespace WebApplication2.Models
{
    public class EventFacultyRole
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int FacultyRoleId { get; set; }
        public FacultyRole FacultyRole { get; set; }

        public string EventFacultyId { get; set; }

        public EventFaculty EventFaculty { get; set; }
    }
}