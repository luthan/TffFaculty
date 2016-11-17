using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Event
    {

        [Key]
        public int Id { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string Type { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string VenueName { get; set; }
        public string VenueAddress1 { get; set; }
        public string VenueAddress2 { get; set; }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
        public string VenueZip { get; set; }
        public string HotelBlockCode { get; set; }
        public int? ArriveHoursBefore { get; set; }
        public int? DepartHoursAfter { get; set; }
        public string HotelAllowance { get; set; }
        public string AirfareTrainAllowance { get; set; }
        public string GroundTransfersAllowance { get; set; }
        public string Status { get; set; }

        public virtual List<EventFaculty> EventUsers { get; set; }
        public virtual List<EventFacultyRole> EventFacultyRoles { get; set; }

        public virtual User Manager { get; set; }
    }
}