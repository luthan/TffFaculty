using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class FacultyRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<EventFacultyRole> EventFacultyRoles { get; set; }
    }
}