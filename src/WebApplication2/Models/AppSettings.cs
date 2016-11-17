using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AppSettings
    {
        public string FromEmail { get; set; }
        public string EmailServer { get; set; }
        public string DefaultPassword { get; set; }
        public string NewFacultyEmailSubject { get; set; }
        public string WebsiteAddress { get; set; }
        public string FacultyRoles { get; set; }
        public string Specialties { get; set; }
    }
}
