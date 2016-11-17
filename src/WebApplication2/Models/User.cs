using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public string Specialty { get; set; }
        public string Affiliation { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string AssistantName { get; set; }
        public string AssistantPhone { get; set; }
        public string AssistantEmail { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string OfficialName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Hotel1Name { get; set; }
        public string Hotel1MemberNumber { get; set; }
        public string Hotel2Name { get; set; }
        public string Hotel2MemberNumber { get; set; }
        public string HotelRoomPreference { get; set; }
        public string Airlinel1Name { get; set; }
        public string Airline1MemberNumber { get; set; }
        public string Airline2Name { get; set; }
        public string Airline2MemberNumber { get; set; }
        public string AirlineSeatingPreference { get; set; }
        //public string PreferredAirport { get; set; }
        //public string PreferredTrainStation { get; set; }
        //public string PreferredCarRentalCompany { get; set; }
        //public string CarRentalCompanyMemberNumber { get; set; }
        public string Status { get; set; }

        public virtual List<EventFaculty> EventFaculty { get; set; }
        public virtual List<Disclosure> Disclosures { get; set; }
        public virtual List<EventFacultyRole> EventFacultyRoles { get; set; }
    }
}
